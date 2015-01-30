using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.Services;
using System.Xml;
using MPSC.Library.Exemplos.Utilidades;

namespace MPSC.Library.Exemplos.Utilidades
{
	public class VerLogWS : WebService
	{
		private const String _stringConexaoTemplate = @"Persist Security Info=True;Data Source=127.0.0.1;Initial Catalog=Master;User ID=WSD3;Password={0};MultipleActiveResultSets=True;";
		private const String _comandoSQL = @"
Select
	L.OBJREF,
	L.OBJREF_WCO_SESSAO,
	L.MENSAGEM,
	L.SUBMENSAGEM,
	L.REGISTRO,
	L.WFK_USUINCL,
	S.Solicitacao
From WSD3{0}.dbo.WSD_WFK_Log L
Inner Join WSD3{0}.dbo.WSD_WCO_Sessao S On S.ObjRef = L.ObjRef_WCO_Sessao";


		[WebMethod]
		//[ScriptMethod(UseHttpGet = true)]
		public XmlDocument MostrarLogProducao()
		{
			return MostrarLog(String.Empty, "M3uPr0ntu@r10");
		}

		[WebMethod]
		public XmlDocument MostrarLogHomologacao()
		{
			return MostrarLog("_HOM", "M3uPr0ntu@r10");
		}

		private XmlDocument MostrarLog(String ambiente, String senha)
		{
			var html = new XMLCreator("html");
			var tbody = CriarCabecalho(ambiente, html);

			var stringConexao = String.Format(_stringConexaoTemplate, senha);
			using (var vSqlConnection = new SqlConnection(stringConexao))
			{
				vSqlConnection.Open();
				using (var vSqlCommand = vSqlConnection.CreateCommand())
				{
					vSqlCommand.CommandText = String.Format(_comandoSQL, ambiente);
					using (var vSqlDataReader = vSqlCommand.ExecuteReader())
					{
						while (vSqlDataReader.Read())
							tbody.Add(TransformarDataReader(vSqlDataReader));

						vSqlDataReader.Close();
					}
				}
				vSqlConnection.Close();
			}
			var xml = new XmlDocument();
			xml.LoadXml(html.GerarXML());
			return xml;
		}

		private static XMLCreator CriarCabecalho(String ambiente, XMLCreator html)
		{
			html.Add("head").Add("title", "Log " + ambiente);
			var table = html.Add("body").Add("Table");
			var th = table.Add("thead").Add("th");
			th.Add("td", "Identificador");
			th.Add("td", "Sessao");
			th.Add("td", "Mensagem");
			th.Add("td", "Registro");
			th.Add("td", "Usuario");
			th.Add("td", "Solicitacao");
			return table.Add("tbody");
		}

		private XMLCreator TransformarDataReader(SqlDataReader vSqlDataReader)
		{
			var tr = new XMLCreator("tr");
			tr.Add("td", Convert.ToString(vSqlDataReader["OBJREF"]));
			tr.Add("td", Convert.ToString(vSqlDataReader["OBJREF_WCO_SESSAO"]));
			tr.Add("td", "<![CDATA[" + Convert.ToString(vSqlDataReader["MENSAGEM"]) + Convert.ToString(vSqlDataReader["SUBMENSAGEM"]) + "]]>");
			tr.Add("td", Convert.ToDateTime(vSqlDataReader["REGISTRO"]).ToString("dd/MM/yyyy HH:mm:ss.fff"));
			tr.Add("td", Convert.ToString(vSqlDataReader["WFK_USUINCL"]));
			tr.Add("td", "<![CDATA[" + MSV_Descompactar(Convert.ToString(vSqlDataReader["Solicitacao"])) + "]]>");
			return tr;
		}


		private static String MSV_Descompactar(String prstMensagem)
		{
			String lstRetorno = String.Empty;
			if (!String.IsNullOrEmpty(prstMensagem))
			{
				Byte[] lbyDescompacta = Convert.FromBase64String(prstMensagem);
				MemoryStream lmsMemoriaStream = new MemoryStream(lbyDescompacta);
				GZipStream lgsZipStream = new GZipStream(lmsMemoriaStream, CompressionMode.Decompress);
				Byte[] lbyBuffer = new Byte[lbyDescompacta.Length];
				Int32 lbyLidos = 0;
				do
				{
					lbyLidos = lgsZipStream.Read(lbyBuffer, 0, lbyDescompacta.Length);
					lstRetorno += Encoding.Default.GetString(lbyBuffer, 0, lbyLidos);
				} while (lbyLidos == lbyDescompacta.Length);
				lgsZipStream.Close();
			}

			return lstRetorno;
		}
	}




	public class XMLCreator
	{
		protected readonly String nome;
		protected readonly String valor;
		protected readonly List<XMLCreator> XMLCreators = new List<XMLCreator>();

		public XMLCreator() : this(null, null) { }
		public XMLCreator(String nome) : this(nome, null) { }
		public XMLCreator(String nome, String valor)
		{
			this.nome = nome;
			if (String.IsNullOrEmpty(valor))
				this.valor = String.Empty;
			else
				this.valor = valor;
		}

		public virtual XMLCreator Add(XMLCreator xmlCreator)
		{
			return (xmlCreator == null) ? this : Add(xmlCreator, String.IsNullOrEmpty(xmlCreator.nome));
		}

		public virtual XMLCreator Add(XMLCreator xmlCreator, Boolean onlyChilds)
		{
			if (onlyChilds || String.IsNullOrEmpty(xmlCreator.nome))
			{
				XMLCreators.AddRange(xmlCreator.XMLCreators);
				xmlCreator = this;
			}
			else
				XMLCreators.Add(xmlCreator);

			return xmlCreator;
		}

		public virtual XMLCreator Add(String nome)
		{
			XMLCreator vXMLCreator = new XMLCreator(nome);
			XMLCreators.Add(vXMLCreator);
			return vXMLCreator;
		}

		public virtual XMLCreator Add(String nome, String valor)
		{
			XMLCreator vXMLCreator = new XMLCreator(nome, valor);
			XMLCreators.Add(vXMLCreator);
			return vXMLCreator;
		}

		public virtual XMLCreator Clone(String nomeClone)
		{
			XMLCreator vXMLCreator = new XMLCreator(nomeClone);
			vXMLCreator.Add(this, true);
			return vXMLCreator;
		}

		public virtual int Count()
		{
			return XMLCreators.Count;
		}

		public virtual String GerarXML()
		{
			return GerarXML(String.Empty).ToString();
		}

		protected virtual StringBuilder GerarXML(String tab)
		{
			return GerarXML(!String.IsNullOrEmpty(this.nome), tab);
		}

		protected virtual StringBuilder GerarXML(Boolean root, String tab)
		{
			StringBuilder vStringBuilder = new StringBuilder();
			Append(vStringBuilder, tab);

			if (root)
				vStringBuilder.AppendFormat("<{0}>", this.nome);

			if (XMLCreators.Count == 0)
				vStringBuilder.Append(this.valor);
			else
			{
				Append(vStringBuilder, "\r\n");
				foreach (XMLCreator vXMLCreator in XMLCreators)
					vStringBuilder.Append(vXMLCreator.GerarXML(tab + "\t"));
				Append(vStringBuilder, tab);
			}

			if (root)
				vStringBuilder.AppendFormat("</{0}>", this.nome);

			Append(vStringBuilder, "\r\n");
			return vStringBuilder;
		}

		protected virtual void Append(StringBuilder stringBuilder, String str)
		{
			stringBuilder.Append(str);
		}

		public override string ToString()
		{
			return this.GerarXML();
		}
	}
}

public static class Teste
{
	public static String Testar()
	{
		var x = new XMLCreator("root");
		var apolices = x.Add("Apolices");
		var apolice = apolices.Add("Apolice");
		apolice.Add("Numero", "123456");
		apolice.Add("Contrato", "654321");
		apolice.Add("VigenciaI", "01/01/2014");
		apolice.Add("VigenciaF", "31/12/2014");
		var coberturas = apolice.Add("Coberturas");

		coberturas.Add(Cob("cob1"), true);
		coberturas.Add(Cob("cob2"));
		coberturas.Add(Cob("cob3"), false);

		coberturas.Add(Cob(""), true);
		coberturas.Add(Cob(""));
		coberturas.Add(Cob(""), false);

		coberturas.Add(Cob(null), true);
		coberturas.Add(Cob(null));
		coberturas.Add(Cob(null), false);

		var s = x.GerarXML();
		return s;
	}

	public static XMLCreator Cob(string nome)
	{
		var coberturas = new XMLCreator(nome);
		var cobertura = coberturas.Add("Cobertura");
		cobertura.Add("Numero", "123456");
		cobertura.Add("Contrato", "654321");
		cobertura.Add("VigenciaI", "01/01/2014");
		cobertura.Add("VigenciaF", "31/12/2014");
		return coberturas;
	}
}