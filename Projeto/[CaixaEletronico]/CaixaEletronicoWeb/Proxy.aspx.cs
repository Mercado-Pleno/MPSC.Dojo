using System;
using System.IO;
using System.Net;
using System.Web;

public partial class Proxy : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			string proxyURL = string.Empty;
			try
			{
				proxyURL = "http://svdatapp31/eSim/CarregaDocumento.aspx";
				proxyURL += "?contrato=" + HttpUtility.UrlDecode(Request.QueryString["contrato"].ToString());
				proxyURL += "&fatura=" + HttpUtility.UrlDecode(Request.QueryString["fatura"].ToString());
				proxyURL += "&produtor=" + HttpUtility.UrlDecode(Request.QueryString["produtor"].ToString());
			}
			catch { }

			if (!String.IsNullOrEmpty(proxyURL))
			{
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(proxyURL);
				request.Method = "GET";
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();

				if (response.StatusCode.ToString().ToLower().Equals("ok"))
				{
					const Int32 BufferLength = 4096;
					Response.ContentType = response.ContentType;
					Stream content = response.GetResponseStream();
					Byte[] buffer = new Byte[BufferLength];
					Int32 bytesLidos = content.Read(buffer, 0, BufferLength);
					while (bytesLidos > 0)
					{
						Response.OutputStream.Write(buffer, 0, bytesLidos);
						bytesLidos = content.Read(buffer, 0, BufferLength);
					}
				}
			}
		}
		catch (Exception exception)
		{
			Response.Write(exception.Message);
		}
	}
}