namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.SqlClient;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Repository;

	public class ClienteController // CRUD
	{
		private IValidador validador = new ValidadorCliente();
		private IRepositorio<Cliente> repositorio = new ClienteRepository();

		public void Incluir(Cliente cliente)
		{ /*
		   * Errado...
			if (String.IsNullOrEmpty(cliente.Nome))
				throw new ArgumentException("Nome do Cliente é nulo");
			if (cliente.Id <= 0)
				throw new ArgumentException("Id do Cliente é nulo");

			string comandoSQL = "Insert Into Cliente (Id, Nome) Values (@Id, @Nome);";
			var comando = conexao.CreateCommand();
			comando.CommandText = comandoSQL;
			var parametroId = comando.CreateParameter();
			parametroId.ParameterName = "Id";
			comando.ExecuteNonQuery();
		   */

			try
			{
				if (validador.Validar(cliente))
					repositorio.Incluir(cliente);
			}
			catch (Exception)
			{
				throw;
			}
		}


		public void Alterar(Cliente cliente)
		{ //Certo
			try
			{
				if (validador.Validar(cliente))
					repositorio.Alterar(cliente);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void Excluir()
		{
		}

		public Entidade Pesquisar()
		{
			return null;
		}

		public IList<Entidade> Listar()
		{
			return null;
		}

	}
}
