using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web.Services;
using System.Xml;

namespace MP.Library.Exemplos.Utilidades
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
		private readonly String _nome;
		private readonly String _valor;
		private readonly List<XMLCreator> XMLCreators = new List<XMLCreator>();

		public XMLCreator() : this(null, null) { }
		public XMLCreator(String prstNome) : this(prstNome, null) { }
		public XMLCreator(String prstNome, String prstValor)
		{
			_nome = prstNome;
			if (String.IsNullOrEmpty(prstValor))
				_valor = String.Empty;
			else
				_valor = prstValor;
		}

		public XMLCreator Add(XMLCreator prXMLCreator)
		{
			return ((prXMLCreator == null) ? this : Add(prXMLCreator, prXMLCreator._nome == null));
		}

		public XMLCreator Add(XMLCreator prXMLCreator, Boolean prboOnlyChilds)
		{
			if (prboOnlyChilds)
			{
				XMLCreators.AddRange(prXMLCreator.XMLCreators);
				prXMLCreator = this;
			}
			else
				XMLCreators.Add(prXMLCreator);

			return prXMLCreator;
		}

		public XMLCreator Add(String prstNome)
		{
			XMLCreator lXMLCreator = new XMLCreator(prstNome);
			XMLCreators.Add(lXMLCreator);
			return lXMLCreator;
		}

		public XMLCreator Add(String prstNome, String prstValor)
		{
			XMLCreator lXMLCreator = new XMLCreator(prstNome, prstValor);
			XMLCreators.Add(lXMLCreator);
			return lXMLCreator;
		}

		public String GerarXML()
		{
			return GerarXML(_nome != null);
		}

		private String GerarXML(Boolean prboRoot)
		{
			StringBuilder lStringBuffer = new StringBuilder();

			if (prboRoot)
			{
				lStringBuffer.Append("<");
				lStringBuffer.Append(_nome);
				lStringBuffer.Append(">");
			}

			if (XMLCreators.Count == 0)
				lStringBuffer.Append(_valor);
			else
				foreach (XMLCreator lXMLCreator in XMLCreators)
					lStringBuffer.Append(lXMLCreator.GerarXML());

			if (prboRoot)
			{
				lStringBuffer.Append("</");
				lStringBuffer.Append(_nome);
				lStringBuffer.Append(">");
			}

			return lStringBuffer.ToString();
		}

		public int Count()
		{
			return XMLCreators.Count;
		}

		public XMLCreator Clone(String nomeClone)
		{
			XMLCreator lXMLCreator = new XMLCreator(nomeClone);
			lXMLCreator.Add(this, true);
			return lXMLCreator;
		}
	}
}