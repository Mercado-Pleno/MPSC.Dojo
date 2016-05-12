using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class LivrariaHarryPotterRunner: IExecutavel
	{
		public void Executar()
		{
			var livrariaHarryPotter = new LivrariaHarryPotter(42, 0, 5, 10, 15, 20);
		}
	}

	public class LivrariaHarryPotter
	{
		private Decimal _valorCadaLivro = 42.00M;
		private Decimal[] _descontosProgressivos;

		public LivrariaHarryPotter(Decimal valorCadaLivro, params Decimal[] descontosPercentuaisProgressivos)
		{
			_valorCadaLivro = valorCadaLivro;
			_descontosProgressivos = descontosPercentuaisProgressivos.Select(d => 1.00M - (d / 100.00M)).ToArray();
		}

		public Decimal ValorAPagar(params IntencaoDeCompra[] titulos)
		{
			var titulosDesagrupados = DesagruparTitulos(titulos);
			var agrupamento = titulosDesagrupados.Distinct(IntencaoDeCompra.Comparador.Instancia).Count();
			return VerificarOMenorValor(agrupamento, titulosDesagrupados);
		}

		private Decimal VerificarOMenorValor(Int32 agrupamento, params IntencaoDeCompra[] titulosDesagrupados)
		{
			if (agrupamento <= 0)
				return 0.00M;
			else if (agrupamento == 1)
				return Calcular(agrupamento, titulosDesagrupados);
			else
			{
				var valor1 = Calcular(agrupamento, titulosDesagrupados);
				var valor2 = VerificarOMenorValor(agrupamento - 1, titulosDesagrupados);

				return Math.Min(valor1, valor2);
			}
		}

		public Decimal Calcular(Int32 quantidadeMaxima, params IntencaoDeCompra[] titulos)
		{
			var conjunto = AgruparTitulosDiferentes(quantidadeMaxima, titulos);
			return CalcularValorDosConjutosDeLivros(conjunto);
		}

		private Decimal CalcularValorDosConjutosDeLivros(IEnumerable<ConjuntoDeLivros> conjuntos)
		{
			var valorTotal = 0.00M;
			foreach (var conjunto in conjuntos)
			{
				var quantidadeDiferente = conjunto.Livros.Distinct(IntencaoDeCompra.Comparador.Instancia).Count();
				var fatorDeDesconto = ObterFatorDeDesconto(quantidadeDiferente);
				foreach (var titulo in conjunto.Livros)
					valorTotal += (titulo.Quantidade * _valorCadaLivro * fatorDeDesconto);
			}

			return valorTotal;
		}

		public IEnumerable<ConjuntoDeLivros> AgruparTitulosDiferentes(Int32 quantidadeMaxima, params IntencaoDeCompra[] titulos)
		{
			var retorno = new List<ConjuntoDeLivros>();
			var livros = titulos.ToList();
			var conjunto = new ConjuntoDeLivros();

			while (livros.Any())
			{
				var livro = livros.FirstOrDefault(l => !conjunto.Livros.Any(cl => cl.Livro == l.Livro));
				if ((livro != null) && (conjunto.Livros.Count < quantidadeMaxima))
				{
					livros.Remove(livro);
					conjunto.Livros.Add(livro);
				}
				else
				{
					retorno.Add(conjunto);
					conjunto = new ConjuntoDeLivros();
				}
			}
			retorno.Add(conjunto);
			return retorno;
		}

		public IntencaoDeCompra[] DesagruparTitulos(params IntencaoDeCompra[] titulos)
		{
			return titulos
				.SelectMany(t => Enumerable.Repeat(new IntencaoDeCompra { Livro = t.Livro, Quantidade = 1 }, t.Quantidade))
				.ToArray();
		}

		public IntencaoDeCompra[] AgruparTitulos(params IntencaoDeCompra[] titulos)
		{
			return titulos
				.GroupBy(t => t.Livro)
				.Select(g => new IntencaoDeCompra { Livro = g.Key, Quantidade = g.Sum(t => t.Quantidade) })
				.ToArray();
		}

		public Decimal ObterFatorDeDesconto(Int32 quantidadeDeLivrosNoPacote)
		{
			var indiceDoDesconto = 0;
			if (quantidadeDeLivrosNoPacote <= 0)
				throw new Exception("Informe uma quantidade Válida");
			else if (quantidadeDeLivrosNoPacote <= _descontosProgressivos.Length)
				indiceDoDesconto = quantidadeDeLivrosNoPacote - 1;
			else
				indiceDoDesconto = _descontosProgressivos.Length - 1;

			return _descontosProgressivos[indiceDoDesconto];
		}
	}

	public class ConjuntoDeLivros
	{
		public List<IntencaoDeCompra> Livros { get; private set; }

		public ConjuntoDeLivros()
		{
			Livros = new List<IntencaoDeCompra>();
		}
	}

	public class IntencaoDeCompra
	{
		public String Livro { get; set; }
		public Int32 Quantidade { get; set; }

		public class Comparador : IEqualityComparer<IntencaoDeCompra>
		{
			public static readonly Comparador Instancia = new Comparador();
			private Comparador() { }
			public Boolean Equals(IntencaoDeCompra x, IntencaoDeCompra y)
			{
				return x.Livro.ToUpper().Equals(y.Livro.ToUpper());
			}

			public Int32 GetHashCode(IntencaoDeCompra obj)
			{
				return obj.Livro.GetHashCode();
			}
		}
	}
}