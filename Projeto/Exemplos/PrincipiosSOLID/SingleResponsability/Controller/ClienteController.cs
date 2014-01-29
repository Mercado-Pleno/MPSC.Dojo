namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Controller
{
	using System;
	using System.Collections.Generic;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Controller.Repository;

	public class ClienteController // CRUD
	{
		private IValidador validador = new ValidadorCliente();
		private IRepositorio<Cliente> repositorio = new ClienteRepository();

		/// <summary>
		/// <code>
		///		// Errado...
		///		if (String.IsNullOrEmpty(cliente.Nome))
		///			throw new ArgumentException("Nome do Cliente é nulo");
		///		if (cliente.Id <![CDATA[<]]>= 0)
		///			dhrow new ArgumentException("Id do Cliente é nulo");
		///
		///		string comandoSQL = "Insert Into Cliente (Id, Nome) Values (@Id, @Nome);";
		///		var comando = conexao.CreateCommand();
		///		comando.CommandText = comandoSQL;
		///		var parametroId = comando.CreateParameter();
		///		parametroId.ParameterName = "Id";
		///		comando.ExecuteNonQuery();
		///	
		///	</code>
		/// </summary>
		/// <param name="cliente"></param>
		public void Incluir(Cliente cliente)
		{
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
		{
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