using System;
using System.Collections.Generic;

namespace MPSC.SVNControl.MockData.DataBaseInterface
{
	public interface ITabela
	{
		String Nome { get; }
	}

	public interface ITabela<Tabela> : ITabela
	{
		IList<Tabela> ListaDados { get; }
		Tabela Adicionar(Tabela dados);
	}

	public class Tabela<T> : ITabela<T>
	{
		public String Nome { get; private set; }
		public IList<T> ListaDados { get; private set; }

		public Tabela() : this(typeof(T).Name) { }
		public Tabela(String nome)
		{
			ListaDados = new List<T>();
			Nome = nome;
		}

		public T Adicionar(T dados)
		{
			ListaDados.Add(dados);
			return dados;
		}
	}
}