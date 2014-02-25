using System.Data;
using System.Data.Common;
using System;

namespace MP.SVNControl.MockData
{
	public class MockCommand : IDbCommand
	{
		public MockCommand(MockConnection mockConnection)
		{
			Connection = mockConnection;
			Parameters = new MockParameterCollection();
		}

		public void Cancel() { }

		public string CommandText { get; set; }

		public int CommandTimeout { get; set; }

		public CommandType CommandType { get; set; }

		public IDbConnection Connection { get; set; }

		public IDbDataParameter CreateParameter()
		{
			return new MockDataParameter();
		}

		public int ExecuteNonQuery()
		{
			var vConnection = Connection as MockConnection;
			var vBancoDeDados = vConnection.BancoDeDados;
			if (CommandType == CommandType.StoredProcedure)
			{
				var vStoredProcedure = vBancoDeDados.Obter(CommandText);
				return vStoredProcedure.Executar(Parameters as MockParameterCollection);
			}
			else if (CommandType == CommandType.Text)
			{
				return 0;
			}
			else
				return 0;
		}

		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			return new MockDataReader(this, behavior);
		}

		public IDataReader ExecuteReader()
		{
			return ExecuteReader(CommandBehavior.Default);
		}

		public object ExecuteScalar()
		{
			var vDataReader = ExecuteReader();
			Object vResult = vDataReader.Read() ? vDataReader[0] : null;
			vDataReader.Close();
			return vResult;
		}

		public IDataParameterCollection Parameters { get; private set; }

		public void Prepare() { }

		public IDbTransaction Transaction { get; set; }

		public UpdateRowSource UpdatedRowSource { get; set; }

		public void Dispose()
		{
			Connection = null;
			Transaction = null;
		}
	}
}