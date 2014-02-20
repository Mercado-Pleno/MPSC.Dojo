using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IBancoDados
	{
		String Nome { get; }
		IList<ITabela> ListaTabela { get; }
		ITabela<T> AdicionarTabela<T>(ITabela<T> tabela);

		IList<IStoredProcedure> ListaStoredProcedure { get; }
		IStoredProcedure Obter(String nomeStoredProcedure);
	}


	public class BancoDados : IBancoDados
	{
		public String Nome { get; private set; }
		public IList<ITabela> ListaTabela { get; private set; }
		public IList<IStoredProcedure> ListaStoredProcedure { get; private set; }
		public BancoDados(String nome)
		{
			Nome = nome;
			ListaTabela = new List<ITabela>();
			ListaStoredProcedure = new List<IStoredProcedure>();
		}

		public ITabela<T> AdicionarTabela<T>(ITabela<T> tabela)
		{
			ListaTabela.Add(tabela);
			return tabela;
		}

		public IStoredProcedure Obter(String nomeStoredProcedure)
		{
			return ListaStoredProcedure.First(sp => sp.Nome.Equals(nomeStoredProcedure));
		}
	}
}