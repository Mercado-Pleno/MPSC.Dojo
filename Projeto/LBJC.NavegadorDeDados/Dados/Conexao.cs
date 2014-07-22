using System;
using System.Data;
using LBJC.NavegadorDeDados.View;

namespace LBJC.NavegadorDeDados
{
	public class Conexao
	{
		private IDbConnection iDbConnection = null;
		private IDbCommand iDbCommand = null;
		public IDataReader iDataReader { get; private set; }

		public IDataReader Executar(String query)
		{
			Free();
			iDbConnection = iDbConnection ?? ObterConexao();
			iDbCommand = CriarComando(iDbConnection, query);
			return (iDataReader = iDbCommand.ExecuteReader());
		}

		private IDbCommand CriarComando(IDbConnection iDbConnection, String query)
		{
			if (iDbConnection.State != ConnectionState.Open)
				iDbConnection.Open();
			IDbCommand iDbCommand = iDbConnection.CreateCommand();
			iDbCommand.CommandText = query;
			iDbCommand.CommandType = CommandType.Text;
			iDbCommand.CommandTimeout = 60;
			return iDbCommand;
		}

		private IDbConnection ObterConexao()
		{
			IDbConnection iDbConnection = null;
			try
			{
				iDbConnection = Autenticacao.Dialog();
			}
			catch (Exception) { }
			return iDbConnection;
		}

		public void Dispose()
		{
			Free();

			if (iDbConnection != null)
			{
				try
				{
					if (iDbConnection.State != ConnectionState.Closed)
						iDbConnection.Close();
					iDbConnection.Dispose();
				}
				catch (Exception) { }
				iDbConnection = null;
			}
		}

		private void Free()
		{
			if (iDataReader != null)
			{
				try
				{
					if (!iDataReader.IsClosed)
						iDataReader.Close();
					iDataReader.Dispose();
				}
				catch (Exception) { }
				iDataReader = null;
			}

			if (iDbCommand != null)
			{
				try
				{
					iDbCommand.Cancel();
					iDbCommand.Dispose();
				}
				catch (Exception) { }
				iDbCommand = null;
			}
		}
	}
}