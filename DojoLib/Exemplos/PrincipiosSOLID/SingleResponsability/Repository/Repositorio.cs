using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Repository
{
	public interface IRepositorio<T>
	{
		void Alterar(T entidade);
		void Incluir(T entidade);
	}

	public abstract class Repositorio<T> : IRepositorio<T> where T: Entidade
	{
		protected DbConnection conexao;

		public Repositorio(DbConnection dbConnection)
		{
			conexao = dbConnection;
		}

		public abstract void Alterar(T entidade);

		public abstract void Incluir(T entidade);
	}
}