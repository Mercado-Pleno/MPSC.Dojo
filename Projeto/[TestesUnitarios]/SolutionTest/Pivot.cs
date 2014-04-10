using System;
using System.Linq;
using System.Collections.Generic;

namespace MP.Library.TestesUnitarios.SolutionTest
{
	public class Pivot<T>
	{
		private Func<T, String> _colunaFixa;
		private Func<T, String> _colunaDinamica;
		private Func<T, Decimal> _calculo;

		public Pivot() { }

		public Pivot<T> AddColumn(Func<T, String> coluna)
		{
			_colunaFixa = coluna;
			return this;
		}

		public Pivot<T> AddGroup(Func<T, String> coluna)
		{
			_colunaDinamica = coluna;
			return this;
		}

		public Pivot<T> Calcular(Func<T, Decimal> funcao)
		{
			_calculo = funcao;
			return this;
		}

		public Object[,] TransformarDataSource(IEnumerable<T> dataSource)
		{
			var linhas = dataSource.Select(_colunaFixa).Distinct().ToList();
			var colunas = dataSource.Select(_colunaDinamica).Distinct().ToList();

			var vQuantidadeDeLinhas = linhas.Count() + 1;
			var vQuantidadeDeColunas = dataSource.Select(_colunaDinamica).Distinct().Count() + 1;
			var vMatriz = new Object[vQuantidadeDeLinhas, vQuantidadeDeColunas];

			foreach (var linha in linhas)
			{
				vMatriz[linhas.IndexOf(linha) + 1, 0] = linha;
				foreach (var coluna in colunas)
				{
					vMatriz[0, colunas.IndexOf(coluna) + 1] = coluna;

					var dados = dataSource.Where(d => _colunaFixa(d) == linha);
					dados = dados.Where(d => _colunaDinamica(d) == coluna);
					var calculo = dados.Sum(_calculo);
					vMatriz[linhas.IndexOf(linha) + 1, colunas.IndexOf(coluna) + 1] = calculo;
				}
			}

			return vMatriz;
		}
	}
}
