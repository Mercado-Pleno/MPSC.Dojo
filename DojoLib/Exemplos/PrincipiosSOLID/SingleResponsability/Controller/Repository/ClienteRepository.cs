namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Controller.Repository
{
	using System;
	using System.Data;
	using System.Data.Common;
	using System.Data.SqlClient;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain;

	public class ClienteRepository : Repositorio<Cliente>
	{
		public ClienteRepository()
			: base(new SqlConnection())
		{
		}

		public override void Incluir(Cliente cliente)
		{
			ProcessarSQL(cliente, "Insert Into Cliente (Id, Nome) Values (@Id, @Nome);");
		}

		public override void Alterar(Cliente cliente)
		{
			ProcessarSQL(cliente, "Update Cliente Set Nome = @Nome Where Id = @Id;");
		}

		private void ProcessarSQL(Cliente cliente, String comandoSQL)
		{
			DbCommand comando = CriarComando(comandoSQL);
			AdicionarParametro(comando, "Id", DbType.Int32, ParameterDirection.Input, cliente.Id);
			AdicionarParametro(comando, "Nome", DbType.String, ParameterDirection.Input, cliente.Nome);
			comando.ExecuteNonQuery();
		}

		private DbCommand CriarComando(String comandoSQL)
		{
			DbCommand comando = conexao.CreateCommand();
			comando.CommandText = comandoSQL;
			comando.CommandType = CommandType.Text;
			return comando;
		}

		private void AdicionarParametro(DbCommand comando, String nomeDoParametro, DbType tipoDoParametro, ParameterDirection direcaoDoParametro, Object valorDoParametro)
		{
			var parametro = comando.CreateParameter();
			parametro.ParameterName = nomeDoParametro;
			parametro.Value = valorDoParametro;
			parametro.DbType = tipoDoParametro;
			parametro.Direction = direcaoDoParametro;
			comando.Parameters.Add(parametro);
		}
	}
}