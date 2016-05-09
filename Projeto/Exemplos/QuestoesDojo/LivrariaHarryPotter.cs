using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class LivrariaHarryPotter
	{
		private const Decimal valorCadaLivro = 42.00M;


		public Decimal ValorAPagar(params IntencaoDeCompra[] titulos)
		{
			var titulosDesagrupados = DesagruparTitulos(titulos);
			var quantidadeDiferente = titulosDesagrupados.Distinct(IntencaoDeCompra.Comparador.Instancia).Count();

			var valorTotal = 0.00M;
			foreach (var titulo in titulosDesagrupados)
			{
				valorTotal += valorCadaLivro * titulo.Quantidade;
			}

			var fatorDeDesconto = ObterFatorDeDesconto(quantidadeDiferente);

			return valorTotal * fatorDeDesconto;
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
			if (quantidadeDeLivrosNoPacote <= 0)
				throw new Exception("Informe uma quantidade Válida");
			else if (quantidadeDeLivrosNoPacote == 1)
				return 1.00M;
			else if (quantidadeDeLivrosNoPacote == 2)
				return 0.95M;
			else if (quantidadeDeLivrosNoPacote == 3)
				return 0.90M;
			else if (quantidadeDeLivrosNoPacote == 4)
				return 0.85M;
			else
				return 0.80M;
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