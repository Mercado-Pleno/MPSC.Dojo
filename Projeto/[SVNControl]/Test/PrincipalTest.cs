using System;
using System.Data;
using MP.SVNControl.MockData;
using MP.SVNControl.MockData.DataBaseInterface;

namespace MP.SVNControl.Test
{
	public static class PrincipalTest
	{
		public static void Main(string[] args)
		{
			var recurso = Recurso.Instancia;
			var servidor = recurso.AdicionarServidor(new Servidor("192.168.0.1", "User", "Senha"));
			var database = servidor.AdicionarBancoDados(new BancoDados("PlenoSMS"));
			var tabela1 = database.AdicionarTabela(new Tabela<Cliente>());
			var tabela2 = database.AdicionarTabela(new Tabela<Documento>());

			tabela1.Adicionar(new Cliente());
			tabela1.Adicionar(new Cliente());
			tabela1.Adicionar(new Cliente());
			tabela2.Adicionar(new Documento());
			tabela2.Adicionar(new Documento());
			tabela2.Adicionar(new Documento());


			var vMockConnection = new MockConnection();
			var vDbCommand = vMockConnection.CreateCommand();
			vDbCommand.CommandText = "Select * From Cliente Where (Nome = @Nome);";
			vDbCommand.CommandType = CommandType.Text;

			var vDbDataParameter = vDbCommand.CreateParameter();
			vDbDataParameter.ParameterName = "Nome";
			vDbDataParameter.DbType = DbType.String;
			vDbDataParameter.Direction = ParameterDirection.Input;
			vDbDataParameter.Value = "Bruno";
			vDbCommand.Parameters.Add(vDbDataParameter);

			var vDataReader = vDbCommand.ExecuteReader();
			while (vDataReader.Read())
			{
				Console.WriteLine("{0} - {1}", vDataReader["Nome"], vDataReader["Idade"]);
			}
			vDataReader.Close();
			vDataReader.Dispose();

			vDbCommand.Dispose();

			vMockConnection.Close();
			vMockConnection.Dispose();
			
		}
	}

	public class Cliente
	{
		public String Nome { get; set; }
		public int Idade { get; set; }
	}

	public class Documento
	{
		public String Numero { get; set; }
	}
}