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
			iDbConnection = iDbConnection ?? Autenticacao.Dialog();
			iDbCommand = CriarComando(iDbConnection, query);
			iDataReader = iDbCommand.ExecuteReader();
			return iDataReader;
		}

		private static IDbCommand CriarComando(IDbConnection iDbConnection, String query)
		{
			if (iDbConnection.State != ConnectionState.Open)
				iDbConnection.Open();
			IDbCommand iDbCommand = iDbConnection.CreateCommand();
			iDbCommand.CommandText = query;
			iDbCommand.CommandType = CommandType.Text;
			iDbCommand.CommandTimeout = 60;
			return iDbCommand;
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
				}
				catch (Exception) { }
				finally { iDbConnection.Dispose(); }
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
					
				}
				catch (Exception) { }
				finally { iDataReader.Dispose(); }
				iDataReader = null;
			}

			if (iDbCommand != null)
			{
				try
				{
					iDbCommand.Cancel();
				}
				catch (Exception) { }
				finally { iDbCommand.Dispose(); }
				iDbCommand = null;
			}
		}
	}
}