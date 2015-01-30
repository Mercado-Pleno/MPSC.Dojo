using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MPSC.SVNControl.MockData.DataBaseInterface
{
	public delegate void A(String a);
	public interface IBancoDados
	{
		String Nome { get; }
		//IList<ITabela> ListaTabela { get; }
		ITabela<T> AdicionarTabela<T>(ITabela<T> tabela);
		ITabela<T> Tabela<T>();

		IList<IStoredProcedure> ListaStoredProcedure { get; }
		IStoredProcedure Obter(String nomeStoredProcedure);

		IInsertInto<Tabela> InsertInto<Tabela>() where Tabela : class, new();
		IInsertInto<T> InsertInto<T>(params Expression<Func<T, Object>>[] lambdas) where T : class, new();
	}

	public interface IInsertInto<T> where T : class, new()
	{
		T Values(params Action<T>[] atribuicoes);
		T Values(params Object[] valores);
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

		public ITabela ObterTabela(String nomeDaTabela)
		{
			return ListaTabela.First(sp => sp.Nome.Equals(nomeDaTabela));
		}

		public ITabela<T> Tabela<T>()
		{
			return ObterTabela(typeof(T).Name) as ITabela<T>;
		}

		public IStoredProcedure Obter(String nomeStoredProcedure)
		{
			return ListaStoredProcedure.First(sp => sp.Nome.Equals(nomeStoredProcedure));
		}

		public IInsertInto<Tabela> InsertInto<Tabela>() where Tabela : class, new()
		{
			var vTabela = Tabela<Tabela>();
			var vLinha = vTabela.Adicionar(new Tabela());
			return new Insert<Tabela>(vLinha);
		}

		public IInsertInto<T> InsertInto<T>(params Expression<Func<T, Object>>[] lambdas) where T : class, new()
		{
			var vTabela = Tabela<T>();
			T linha = vTabela.Adicionar(new T());

			List<PropertyInfo> lista = new List<PropertyInfo>();
			
			foreach (var lambda in lambdas)
				lista.Add(InsertIntoImpl2<T>(lambda.Body.ToString(), lambda.Parameters[0].Type));

			return new Insert<T>(linha, lista);
		}

		private PropertyInfo InsertIntoImpl2<Tabela>(String lambdaExpression, Type type) where Tabela : class
		{
			var vNomePropriedade = lambdaExpression.Substring(lambdaExpression.LastIndexOf(".") + 1).Replace(")", "");
			return type.GetProperty(vNomePropriedade);
		}



		private class Insert<T> : IInsertInto<T> where T : class, new()
		{
			private T _linha;
			private List<PropertyInfo> _lista;

			public Insert(T linha)
			{
				_linha = linha;
			}

			public Insert(T linha, List<PropertyInfo> lista)
			{
				_linha = linha;
				_lista = lista;
			}

			public T Values(params Action<T>[] atribuicoes)
			{
				foreach (var campo in atribuicoes)
					campo.Invoke(_linha);
				return _linha;
			}

			public T Values(params Object[] valores)
			{
				if (valores.Length != _lista.Count)
					throw new ArgumentException("Numero de parâmetros inválidos");
				for (int i = 0; i < valores.Length; i++)
					_lista[i].SetValue(_linha, valores[i], null);

				return _linha as T;
			}

		}
	}
}