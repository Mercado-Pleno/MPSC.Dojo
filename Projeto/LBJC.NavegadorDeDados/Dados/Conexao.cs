using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using IBM.Data.DB2.iSeries;

namespace LBJC.NavegadorDeDados
{
	public class Conexao
	{
		public IDataReader Executar(String query)
		{
			IDbConnection iDbConnection = ObterConexao();
			IDbCommand iDbCommand = CriarComando(iDbConnection, query);
			return iDbCommand.ExecuteReader();
		}

		private IDbCommand CriarComando(IDbConnection iDbConnection, String query)
		{
			if (iDbConnection.State != ConnectionState.Closed)
				iDbConnection.Close();
			if (iDbConnection.State == ConnectionState.Closed)
				iDbConnection.Open();
			IDbCommand iDbCommand = iDbConnection.CreateCommand();
			iDbCommand.CommandText = query;
			iDbCommand.CommandType = CommandType.Text;
			iDbCommand.CommandTimeout = 30;
			return iDbCommand;
		}

		private IDbConnection ObterConexao()
		{
			IDbConnection iDbConnection = null;
			try
			{
				iDbConnection = new ConnectionString().ObterConexao<iDB2Connection>("10.21.4.52", "eSim", "UsrBen", "@poiuy");
				//iDbConnection = new ConnectionString().ObterConexao<OleDbConnection>("10.21.4.52", "eSim", "UsrBen", "@poiuy");
				//iDbConnection = new ConnectionString().ObterConexao<SqlConnection>("mssql.mercadopleno.com.br", "PlenoSMS", "PlenoSMS", "PlenoSMS");
			}
			catch (Exception) { }
			return iDbConnection;
		}

		public void Dispose()
		{
			
		}
	}

	public class ConnectionString
	{
		public String Server { get; private set; }
		public String DataBase { get; private set; }
		public String Usuario { get; private set; }
		public String Senha { get; private set; }

		public IDbConnection ObterConexao<TConnection>(String server, String dataBase, String usuario, String senha) where TConnection : IDbConnection
		{
			Server = server;
			DataBase = dataBase;
			Usuario = usuario;
			Senha = senha;
			return ObterConexao(typeof(TConnection));
		}

		public IDbConnection ObterConexao<TConnection>() where TConnection : IDbConnection
		{
			return ObterConexao(typeof(TConnection));
		}

		private IDbConnection ObterConexao(Type tipo)
		{
			var templateStingConexao = ObterTemplate(tipo);
			var stringConexao = String.Format(templateStingConexao, Server, DataBase, Usuario, Senha);
			return Activator.CreateInstance(tipo, stringConexao) as IDbConnection;
		}

		private String ObterTemplate(Type tipo)
		{
			String retorno = "";
			if (tipo == typeof(SqlConnection))
				retorno = "Persist Security Info=True;Data Source={0};Initial Catalog={1};User ID={2};Password={3};MultipleActiveResultSets=True;";
			else if (tipo == typeof(iDB2Connection))
				retorno = "DataSource={0};UserID={2};Password={3};DataCompression=True;SortSequence=SharedWeight;SortLanguageId=PTG;DefaultCollection={1};";
			else if (tipo == typeof(OleDbConnection))
				retorno = "Provider=IBMDA400;Data Source={0};Default Collection={1};User ID={2};Password={3}";

			return retorno;
		}
	}
}