namespace MPSC.Library.Exemplos.Utilidades
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	public class SeparaListaEMailsERemoveDuplicados : IExecutavel
	{
		public void Executar()
		{
			StreamReader lStreamReader = new StreamReader(@"C:\lista.txt");
			String texto = lStreamReader.ReadToEnd();
			lStreamReader.Close();
			lStreamReader.Dispose();

			IList<String> listaEmails = texto.Replace("\r", "").Replace(" ", "").Split("\n".ToCharArray());
			IList<String> listaEmailsGrava = new List<String>();
			foreach (String eMail in listaEmails)
			{
				String mail = eMail;
				if (eMail.Contains("<") && eMail.Contains(">"))
				{
					mail = eMail.Substring(eMail.IndexOf("<") + 1);
					mail = mail.Substring(0, mail.IndexOf(">"));
				}
				else if (eMail.Contains("@"))
					mail = eMail.Replace(",", "");

				listaEmailsGrava.Add(mail);
			}

			listaEmails = listaEmailsGrava.OrderBy(e => e).Distinct().ToList();
			StreamWriter lStreamWriter = new StreamWriter(@"C:\lista2.txt", false);
			foreach (String eMail in listaEmails)
			{
				lStreamWriter.WriteLine(eMail + ",");
			}

			lStreamWriter.Flush();
			lStreamWriter.Close();
			lStreamWriter.Dispose();
		}
	}
}