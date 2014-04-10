namespace MPSC.Library.Exemplos.Transformacao
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class TestaPivot : IExecutavel
	{
		public void Executar()
		{
			var vPivot = new Pivot<InformacaoDTO>()
				.DefinirColunaFixa(d => d.CodigoRecurso)
				.DefinirAgrupamento(d => d.Regiao)
				.Mostrar(d => d.ValorRegiao);

			var dadosOriginais = Dados.GetDataSource().ToList();
			var vDados = vPivot.TransformarDataSource(dadosOriginais);
			vPivot.Print(vDados);
		}

		public static class Dados
		{
			public static IEnumerable<InformacaoDTO> GetDataSource()
			{
				yield return new InformacaoDTO("0000001", "Alimentacao", "RJ", 0);
				yield return new InformacaoDTO("0000001", "Alimentacao", "SP", 0);
				yield return new InformacaoDTO("0000001", "Alimentacao", "MG", 0);
				yield return new InformacaoDTO("0000001", "Alimentacao", "BA", 1);

				yield return new InformacaoDTO("0000002", "Vestuario", "RJ", 0);
				yield return new InformacaoDTO("0000002", "Vestuario", "SP", 1);
				yield return new InformacaoDTO("0000002", "Vestuario", "MG", 1);
				yield return new InformacaoDTO("0000002", "Vestuario", "BA", 0);

			}

		}

		public class InformacaoDTO
		{
			public String CodigoRecurso { get; set; }
			public String Descricao { get; set; }
			public String Regiao { get; set; }
			public int TipoRegiao { get; set; }
			public String ValorRegiao { get { return TipoRegiao == 0 ? "Reg" : "Bra"; } }

			public InformacaoDTO(String codigoRecurso, String descricao, String regiao, int tipoRegiao)
			{
				CodigoRecurso = codigoRecurso;
				Descricao = descricao;
				Regiao = regiao;
				TipoRegiao = tipoRegiao;
			}
		}
	}

	public class Pivot<T>
	{
		private Func<T, String> _colunaFixa;
		private Func<T, String> _colunaDinamica;
		private Func<T, Decimal> _campo;
		private Func<T, String> _mostrar;

		public Pivot() { }

		public Pivot<T> DefinirColunaFixa(Func<T, String> coluna)
		{
			_colunaFixa = coluna;
			return this;
		}

		public Pivot<T> DefinirAgrupamento(Func<T, String> coluna)
		{
			_colunaDinamica = coluna;
			return this;
		}

		public Pivot<T> Somar(Func<T, Decimal> funcao)
		{
			_campo = funcao;
			return this;
		}

		public Pivot<T> Mostrar(Func<T, String> funcao)
		{
			_mostrar = funcao;
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
					//var calculo = dados.Sum(_campo);

					var informacao = dados.Select(_mostrar).FirstOrDefault();

					vMatriz[linhas.IndexOf(linha) + 1, colunas.IndexOf(coluna) + 1] = informacao;
				}
			}

			return vMatriz;
		}

		public void Print(Object[,] matriz)
		{
			var linhas = matriz.GetUpperBound(0);
			var colunas = matriz.GetUpperBound(1);

			Console.Clear();
			for (int linha = 0; linha <= linhas; linha++)
			{
				for (int coluna = 0; coluna <= colunas; coluna++)
				{
					var dados = matriz[linha, coluna] ?? "";
					Console.Write(dados.ToString().PadLeft(11));
				}
				Console.WriteLine();
			}
		}
	}
}
