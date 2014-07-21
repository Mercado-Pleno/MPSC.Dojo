using System;
using System.Linq;
using System.Collections.Generic;

namespace LBJC.NavegadorDeDados
{
	public static class Extensions
	{
		public const String TokenKeys = "\r\n\t(){}[] ";

		public static String Concatenar<T>(this IEnumerable<T> source, String join)
		{
			return String.Join<T>(join, source);
		}

		public static String ObterApelidoAntesDoPonto(String query, Int32 selectionStart)
		{
			query = query.ToUpper().Insert(selectionStart, ".");

			Int32 i = selectionStart;
			while (!TokenKeys.Contains(query[--i - 1])) ;

			return query.Substring(i, selectionStart - i);
		}

		public static String ObterNomeTabelaPorApelido(String query, Int32 selectionStart, String apelido)
		{
			String nomeDaTabela = String.Empty;
			query = query.ToUpper().Insert(selectionStart, ".");
			var tokens = query.Split(Extensions.TokenKeys.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

			var index = tokens.LastIndexOf(apelido);
			if (tokens.Count > 1)
			{
				if (tokens[index - 1].Equals("AS"))
					nomeDaTabela = tokens[index - 2];
				else if (tokens[index - 1].Equals("FROM") || tokens[index - 1].Equals("JOIN"))
					nomeDaTabela = tokens[index];
				else
					nomeDaTabela = tokens[index - 1];
			}

			return nomeDaTabela;
		}

		public static IEnumerable<String> ListarColunasDasTabelas(Conexao conexao, String tabela)
		{
			var dataReader = conexao.Executar("Select * From " + tabela + " Where 0=1");
			var colunas = dataReader.FieldCount;
			for (Int32 i = 0; i < colunas; i++)
				yield return dataReader.GetName(i);

			dataReader.Close();
			dataReader.Dispose();
		}


	}
}