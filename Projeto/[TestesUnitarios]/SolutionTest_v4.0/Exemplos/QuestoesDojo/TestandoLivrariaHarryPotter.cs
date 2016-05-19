using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;
using System;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoLivrariaHarryPotter
	{
		[TestMethod]
		public void QuandoAgrupa1Livro1Titulos()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			var agrupado = livraria.AgruparTitulos(new IntencaoDeCompra { Livro = "A", Quantidade = 1 });
			var desagrupado = livraria.DesagruparTitulos(agrupado);

			Assert.AreEqual(1, agrupado.Length);
			Assert.AreEqual("A", agrupado[0].Livro);
			Assert.AreEqual(1, agrupado[0].Quantidade);

			Assert.IsTrue(desagrupado.All(t => t.Quantidade == 1));
			Assert.AreEqual(1, desagrupado.Length);
			Assert.AreEqual("A", desagrupado[0].Livro);
			Assert.IsTrue(desagrupado.All(t => t.Livro == "A"));
			Assert.AreEqual("A", desagrupado.Distinct().Single().Livro);
		}

		[TestMethod]
		public void QuandoAgrupa2Livros1Titulos()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			var agrupado = livraria.AgruparTitulos(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 5 });
			var desagrupado = livraria.DesagruparTitulos(agrupado);

			Assert.AreEqual(1, agrupado.Length);
			Assert.AreEqual("A", agrupado[0].Livro);
			Assert.AreEqual(6, agrupado[0].Quantidade);

			Assert.IsTrue(desagrupado.All(t => t.Quantidade == 1));
			Assert.AreEqual(6, desagrupado.Length);
			Assert.IsTrue(desagrupado.All(t => t.Livro == "A"));
			Assert.AreEqual("A", desagrupado.Distinct().Single().Livro);
		}

		[TestMethod]
		public void QuandoAgrupa5Livros1Titulos()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			var agrupado = livraria.AgruparTitulos(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 3 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 5 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 8 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 13 });
			var desagrupado = livraria.DesagruparTitulos(agrupado);

			Assert.AreEqual(1, agrupado.Length);
			Assert.AreEqual("A", agrupado[0].Livro);
			Assert.AreEqual(31, agrupado[0].Quantidade);

			Assert.IsTrue(desagrupado.All(t => t.Quantidade == 1));
			Assert.AreEqual(31, desagrupado.Length);
			Assert.IsTrue(desagrupado.All(t => t.Livro == "A"));
			Assert.AreEqual("A", desagrupado.Distinct().Single().Livro);
		}

		[TestMethod]
		public void QuandoAgrupa5Livros2Titulos()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			var agrupado = livraria.AgruparTitulos(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 3 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 5 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 8 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 13 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 21 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 34 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 55 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 89 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 144 });
			var desagrupado = livraria.DesagruparTitulos(agrupado);

			Assert.AreEqual(2, agrupado.Length);
			Assert.AreEqual("A", agrupado[0].Livro);
			Assert.AreEqual(31, agrupado[0].Quantidade);
			Assert.AreEqual("B", agrupado[1].Livro);
			Assert.AreEqual(343, agrupado[1].Quantidade);

			Assert.IsTrue(desagrupado.All(t => t.Quantidade == 1));
			Assert.AreEqual(374, desagrupado.Length);
			Assert.IsTrue(desagrupado.Any(t => t.Livro == "A"));
			Assert.IsTrue(desagrupado.Any(t => t.Livro == "A"));
			Assert.AreEqual("A", desagrupado.Distinct().First().Livro);
			Assert.AreEqual("B", desagrupado.Distinct().Last().Livro);
		}


		[TestMethod]
		public void QuandoCompraApenasUmLivro()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(1.0M, livraria.ObterFatorDeDesconto(1));
		}

		[TestMethod]
		public void QuandoCompraDoisLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.95M, livraria.ObterFatorDeDesconto(2));
		}

		[TestMethod]
		public void QuandoCompraTresLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.90M, livraria.ObterFatorDeDesconto(3));
		}

		[TestMethod]
		public void QuandoCompraQuatroLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.85M, livraria.ObterFatorDeDesconto(4));
		}

		[TestMethod]
		public void QuandoCompraCincoLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(5));
		}

		[TestMethod]
		public void QuandoCompraMaisDeCincoLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(6));
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(7));
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(8));
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(100));
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(1000));
		}

		[TestMethod, ExpectedException(typeof(Exception))]
		public void QuandoCompraUmaQuantidadeIgualAZeroDeveLancarExcecao()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(0));
			Assert.Fail("Deveria Lancar Excecao e não lancou");
		}

		[TestMethod, ExpectedException(typeof(Exception))]
		public void QuandoCompraUmaQuantidadeMenorQueZeroDeveLancarExcecao()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(0.80M, livraria.ObterFatorDeDesconto(-1));
			Assert.Fail("Deveria Lancar Excecao e não lancou");
		}

		[TestMethod]
		public void QuandoCalculaACompraApenasUmLivro()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(42.00M, livraria.ValorAPagar(new IntencaoDeCompra { Livro = "A", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeDoisLivrosDoMesmoTituloManeira1()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(84.00M, livraria.ValorAPagar(new IntencaoDeCompra { Livro = "A", Quantidade = 2 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeDoisLivrosDoMesmoTituloManeira2()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(84.00M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeDoisLivrosDeTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(79.80M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeTresLivrosDeTresTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(113.4M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeTresLivrosDeDoisTitulosDiferentesManeira1()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(121.80M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeTresLivrosDeDoisTitulosDiferentesManeira2()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(121.80M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeQuatroLivrosDeDoisTitulosDiferentesManeira1()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(159.60M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 2 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeQuatroLivrosDeDoisTitulosDiferentesManeira2()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(159.60m, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeQuatroLivrosDeTresTitulosDiferentesManeira1()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(155.40M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeQuatroLivrosDeTresTitulosDiferentesManeira2()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(155.40M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeQuatroLivrosDeQuatroTitulosDiferentes()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(142.80M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "D", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraDeAcordoComAsInformacoesDoDOJO()
		{
			var livraria = new LivrariaHarryPotter(42.00M, 0M, 5M, 10M, 15M, 20M);
			Assert.AreEqual(281.40M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "D", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "E", Quantidade = 1 }));
		}


		[TestMethod]
		public void QuandoCalculaACompraComOutrosValoresEEoutrosPercentuais()
		{
			var livraria = new LivrariaHarryPotter(10.00M, 0M, 1M, 2M, 20M, 21M);
			Assert.AreEqual(64.00M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "D", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "E", Quantidade = 1 }));
		}

		[TestMethod]
		public void QuandoCalculaACompraComOutrosValoresEEoutrosPercentuais2()
		{
			var livraria = new LivrariaHarryPotter(10.00M, 0M, 1M, 2M, 3M, 30M);
			Assert.AreEqual(64.40M, livraria.ValorAPagar(
				new IntencaoDeCompra { Livro = "A", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "B", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "C", Quantidade = 2 },
				new IntencaoDeCompra { Livro = "D", Quantidade = 1 },
				new IntencaoDeCompra { Livro = "E", Quantidade = 1 }));
		}
	}
}