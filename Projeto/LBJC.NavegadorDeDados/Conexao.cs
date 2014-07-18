using System;
using System.Data;
using System.Data.OleDb;
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
			try
			{
				String vStringConexao = ObterStringConexao(false);
				return new iDB2Connection(vStringConexao);
			}
			catch (Exception)
			{
				String vStringConexao = ObterStringConexao(true);
				return new OleDbConnection(vStringConexao);
			}
		}

		private string ObterStringConexao(Boolean oleDB)
		{
			var server = "10.21.4.52";
			var dataBase = "eSim";
			var usuario = "UsrBen";
			var senha = "@poiuy";
			var strTemplate = (oleDB ? "Provider=IBMDA400;Data Source={0};Default Collection={1};User ID={2};Password={3}" : "DataSource={0};UserID={2};Password={3};DataCompression=True;SortSequence=SharedWeight;SortLanguageId=PTG;DefaultCollection={1};");
			return String.Format(strTemplate, server, dataBase, usuario, senha);
		}
	}
}