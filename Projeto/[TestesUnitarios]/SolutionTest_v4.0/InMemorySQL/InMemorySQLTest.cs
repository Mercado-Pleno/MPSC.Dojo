using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.SVNControl.MockData;
using MP.SVNControl.MockData.DataBaseInterface;

namespace MP.SVNControl.Test
{
	[TestClass]
	public class InMemorySQLTest
	{
		private IBancoDados database;
		private ITabela<Cliente> tabelaCliente;
		private ITabela<Documento> tabelaDocumento;

		[ClassInitialize()]
		public void InMemorySQLTestInitialize()
		{
			var recurso = Recurso.Instancia;
			var servidor = recurso.AdicionarServidor(new Servidor("192.168.0.1", "User", "Senha"));
			database = servidor.AdicionarBancoDados(new BancoDados("PlenoSMS")) as BancoDados;
			tabelaCliente = database.AdicionarTabela(new Tabela<Cliente>());
			tabelaDocumento = database.AdicionarTabela(new Tabela<Documento>());
		}

		[TestInitialize()]
		public void MyTestInitialize()
		{
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirSemParametrosNoInsertInto()
		{
			var bruno = database.InsertInto<Cliente>().Values(e => e.Nome = "Bruno", e => e.Idade = 31);
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirComParametrosNoInsertIntoNaOrdem()
		{
			var luani = database.InsertInto<Cliente>(c => c.Nome, c => c.Idade).Values("Luani", 29);
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirUmObjetoDeClienteSemEstarPreenchido()
		{
			var cliente = tabelaCliente.Adicionar(new Cliente());
			cliente.Nome = "Maria Rita";
			cliente.Idade = 40;
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirUmObjetoDeDocumentoSemEstarPreenchido()
		{
			var documento = tabelaDocumento.Adicionar(new Documento());
			documento.Numero = "DocRita";
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirUmObjetoDeClientePreenchido()
		{
			tabelaCliente.Adicionar(new Cliente() { Nome = "Solange", Idade = 40 });
		}

		[TestMethod]
		public void VerificarSeConsegueIncluirUmObjetoDeDocumentoPreenchido()
		{
			tabelaDocumento.Adicionar(new Documento() { Numero = "DocSolange" });
		}

		[TestMethod]
		public void OutrosTestes()
		{
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
		public void FazerAlgo() { Console.WriteLine("oi"); }
	}

	public class Documento
	{
		public String Numero { get; set; }
	}
}