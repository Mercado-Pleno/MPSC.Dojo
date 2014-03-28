using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using MP.LBJC.ServicoWindows.Interface;

namespace MP.LBJC.Utils
{
	public class NTPClient : IProcessador
	{
		[DllImport("kernel32.dll")]
		private extern static uint SetSystemTime(ref SYSTEMTIME lpSystemTime);

		public Boolean Processar()
		{
			NTPClient.SetSystemTime();
			return true;
		}

		public void Dispose() { }

		public static DateTime SetSystemTime()
		{
			var start = DateTime.Now;
			DateTime vDataHoraOficial = NtpWS_GetNetworkTime();
			var duracao = DateTime.Now - start;
			var hora = new SYSTEMTIME(vDataHoraOficial.AddTicks(duracao.Ticks));
			SetSystemTime(ref hora);
			return vDataHoraOficial;
		}

		private static DateTime NtpWS_GetNetworkTime()
		{
			DateTime dataHora = DateTime.Now;
			string url = @"http://mercadopleno.dlinkddns.com:81/ws/PlenoSMS.asmx/ObterDataHoraOficial";
			try
			{
				String dataEHoraUTC = Enviar(url);
				dataEHoraUTC = dataEHoraUTC.Substring(0, dataEHoraUTC.LastIndexOf("</"));
				dataEHoraUTC = dataEHoraUTC.Substring(dataEHoraUTC.LastIndexOf(">") + 1);
				dataHora = Convert.ToDateTime(dataEHoraUTC);
			}
			catch (Exception) { }
			return dataHora.ToLocalTime();
		}

		private static string Enviar(String endereco)
		{
			StringBuilder vStringBuilder = new StringBuilder();

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endereco);
			request.Method = "GET";
			request.ContentType = "text/xml; charset=iso-8859-1";
			HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			Stream resStream = response.GetResponseStream();

			byte[] buf = new byte[8192];
			int count = resStream.Read(buf, 0, buf.Length);
			while (count > 0)
			{
				vStringBuilder.Append(Encoding.Default.GetString(buf, 0, count));
				count = resStream.Read(buf, 0, buf.Length);
			}
			resStream.Close();
			resStream.Dispose();
			response.Close();

			return vStringBuilder.ToString();
		}

		private struct SYSTEMTIME
		{
			public ushort wYear, wMonth, wDayOfWeek, wDay, wHour, wMinute, wSecond, wMilliseconds;
			public SYSTEMTIME(DateTime data)
			{
				data = data.ToUniversalTime();
				wYear = (ushort)data.Year;
				wMonth = (ushort)data.Month;
				wDayOfWeek = (ushort)data.DayOfWeek;
				wDay = (ushort)data.Day;
				wHour = (ushort)data.Hour;
				wMinute = (ushort)data.Minute;
				wSecond = (ushort)data.Second;
				wMilliseconds = (ushort)data.Millisecond;
			}
		}
	}
}