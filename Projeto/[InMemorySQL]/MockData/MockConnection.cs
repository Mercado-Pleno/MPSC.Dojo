using System;
using System.Data;
using MP.SVNControl.MockData.DataBaseInterface;

namespace MP.SVNControl.MockData
{
	public class MockConnection : IDbConnection
	{
		private IRecurso _recurso = null;
		private String _connectionString = String.Empty;


		private String Servidor { get; set; }
		public String Database { get; private set; }
		private String Usuario { get; set; }
		private String Senha { get; set; }
		private Boolean PersistSecurityInfo { get; set; }
		private Boolean MultipleActiveResultSets { get; set; }
		public int ConnectionTimeout { get; private set; }
		public ConnectionState State { get; private set; }


		public IBancoDados BancoDeDados { get; private set; }

		public MockConnection()
		{
			_recurso = Recurso.Instancia;
		}

		public String ConnectionString
		{
			get
			{
				return "Data Source=" + Servidor
					+ ";Initial Catalog=" + Database
					+ ";User ID=" + Usuario
					+ ";Password=" + Senha
					+ "Persist Security Info=" + PersistSecurityInfo
					+ ";Persist Security Info" + MultipleActiveResultSets;
			}
			set
			{
				var stringConexao = value + ";";
				Servidor = Util.Extrair(stringConexao, "Data Source=", ";");
				Database = Util.Extrair(stringConexao, "Initial Catalog=", ";");
				Usuario = Util.Extrair(stringConexao, "User ID=", ";");
				Senha = Util.Extrair(stringConexao, "Password=", ";");
				PersistSecurityInfo = Util.Extrair(stringConexao, "Persist Security Info=", ";").ToLower().Equals("true");
				MultipleActiveResultSets = Util.Extrair(stringConexao, "Persist Security Info=", ";").ToLower().Equals("true");
				ConnectionTimeout = Convert.ToInt32(Util.Extrair(stringConexao, "TimeOut=", ";"));

				if (State != ConnectionState.Closed)
					State = ConnectionState.Broken;
			}
		}

		public IDbCommand CreateCommand()
		{
			return new MockCommand(this);
		}

		public void ChangeDatabase(String databaseName)
		{
			Database = databaseName;
			if (State != ConnectionState.Closed)
				State = ConnectionState.Broken;
		}

		public void Open()
		{
			BancoDeDados = ObterBancoDeDadosAtivo(Servidor, Database);
			State = ConnectionState.Open;
		}

		private IBancoDados ObterBancoDeDadosAtivo(String servidor, String bancoDeDados)
		{
			return _recurso.Obter(servidor).Obter(bancoDeDados);
		}

		public void Close()
		{
			_recurso = null;
			State = ConnectionState.Closed;
		}

		public void Dispose()
		{
			_recurso = null;
			State = ConnectionState.Closed;
		}

		public IDbTransaction BeginTransaction(IsolationLevel il)
		{
			throw new NotImplementedException();
		}

		public IDbTransaction BeginTransaction()
		{
			return BeginTransaction(IsolationLevel.ReadCommitted);
		}
	}

	public static class Util
	{
		public enum ExtrairEnum
		{
			eNenhum = 0,
			eIgnoraCase = 1,
			eIgnoraFim = 2,
			eRetornaChave = 4,
			eTodos = 7,
		}

		public static int Valor(Enum enumerado)
		{
			return (int)((Object)enumerado);
		}

		public static Boolean Contem(Enum enumerado, Enum enumProcurado)
		{
			int vValorGrd = Valor(enumerado);
			int vValorPeq = Valor(enumProcurado);
			return (((vValorPeq > 0) || (vValorGrd == vValorPeq)) && ((vValorGrd & vValorPeq) == vValorPeq));
		}
		public static String ExtrairXML(String xml, String elemento) { return Extrair(xml, "<" + elemento + ">", "</" + elemento + ">", 1, ExtrairEnum.eNenhum); }
		public static String ExtrairXML(String xml, String elemento, uint ocorrencia) { return Extrair(xml, "<" + elemento + ">", "</" + elemento + ">", ocorrencia, ExtrairEnum.eNenhum); }
		public static String ExtrairXML(String xml, String elemento, uint ocorrencia, ExtrairEnum extrairEnumId) { return Extrair(xml, "<" + elemento + ">", "</" + elemento + ">", ocorrencia, extrairEnumId); }

		public static String Extrair(String origem, String inicio) { return Extrair(origem, inicio, String.Empty, 1, ExtrairEnum.eNenhum); }
		public static String Extrair(String origem, String inicio, String final) { return Extrair(origem, inicio, final, 1, ExtrairEnum.eNenhum); }
		public static String Extrair(String origem, String inicio, String final, uint ocorrencia) { return Extrair(origem, inicio, final, ocorrencia, ExtrairEnum.eNenhum); }
		public static String Extrair(String origem, String inicio, String final, uint ocorrencia, ExtrairEnum extrairEnumId)
		{
			Boolean ignoraCase = Contem(extrairEnumId, ExtrairEnum.eIgnoraCase);
			Boolean ignoraFim = Contem(extrairEnumId, ExtrairEnum.eIgnoraFim);
			Boolean retornaChave = Contem(extrairEnumId, ExtrairEnum.eRetornaChave);

			origem = ((ocorrencia <= 0) || String.IsNullOrWhiteSpace(origem)) ? "" : origem;
			String vOrigem = origem;
			String vIni = inicio;
			String vFim = final;

			if (ignoraCase)
			{
				vOrigem = vOrigem.ToUpper();
				vIni = vIni.ToUpper();
				vFim = vFim.ToUpper();
			}

			while ((vOrigem != "") && (ocorrencia > 0) && (vIni != ""))
			{
				ocorrencia--;
				int vPos = vOrigem.IndexOf(vIni);
				if (vPos >= 0)
				{
					vOrigem = vOrigem.Substring(vPos + vIni.Length);
					origem = origem.Substring(vPos + vIni.Length);
				}
				else
				{
					vOrigem = "";
					origem = "";
				}
			}

			if ((vOrigem != "") && (vFim != ""))
			{
				int vPos = vOrigem.IndexOf(vFim);
				if (vPos >= 0)
				{
					vOrigem = vOrigem.Substring(0, vPos);
					origem = origem.Substring(0, vPos);
				}
				else if (!ignoraFim)
				{
					vOrigem = "";
					origem = "";
					retornaChave = false;
				}
				if (retornaChave)
					origem = inicio + origem + final;
			}

			return origem;
		}
	}
}