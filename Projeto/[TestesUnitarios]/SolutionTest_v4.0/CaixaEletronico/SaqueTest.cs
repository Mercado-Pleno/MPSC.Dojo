using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico;
using MP.Library.CaixaEletronico.Notas;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass]
	public class SaqueTest
	{
		[TestMethod]
		public void Se_Sacar_10_Reais_Deve_Retornar_1_Nota_de_10_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.InformarQuantidade(10, new Nota100());
			caixaForte.InformarQuantidade(2, new Nota050());
			caixaForte.InformarQuantidade(1, new Nota020());
			caixaForte.InformarQuantidade(1, new Nota010());
			Saque saque = new Saque(caixaForte);

			var notas = saque.Sacar(10);

			Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
			Assert.AreEqual(10, notas.Sum(n => n.Valor), "Valor do Saque");
			Assert.IsInstanceOfType(notas[0], typeof(Nota010), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_20_Reais_Deve_Retornar_1_Nota_de_20_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(20);

			Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
			Assert.AreEqual(20, notas.Sum(n => n.Valor), "Valor do Saque");
			Assert.IsInstanceOfType(notas[0], typeof(Nota020), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_50_Reais_Deve_Retornar_1_Nota_de_50_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			var notas = saque.Sacar(50);

			Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
			Assert.AreEqual(50, notas.Sum(n => n.Valor), "Valor do Saque");
			Assert.IsInstanceOfType(notas[0], typeof(Nota050), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_100_Reais_Deve_Retornar_1_Nota_de_100_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(100);

			Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
			Assert.AreEqual(100, notas.Sum(n => n.Valor), "Valor do Saque");
			Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
		}

		[TestMethod, ExpectedException(typeof(SaqueException))]
		public void Se_Tentar_Sacar_Centavos_ou_Valores_Menores_Que_10_Reais_Deve_Disparar_Excecao()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(7);
		}

		[TestMethod, ExpectedException(typeof(SaqueException), "Nao Disparou Exception!")]
		public void Se_Tentar_Sacar_Valores_Maiores_e_Nao_Multiplos_de_10_Reais_Deve_Disparar_Excecao()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(32);
		}

		[TestMethod]
		public void Se_Sacar_30_Reais_Deve_Retornar_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(30);

			Assert.AreEqual(2, notas.Count, "Quantidade de Notas");

			Assert.AreEqual(30, notas.Sum(n => n.Valor), "Valor do Saque");

			Assert.IsInstanceOfType(notas[0], typeof(Nota020), "Tipo da nota");
			Assert.IsInstanceOfType(notas[1], typeof(Nota010), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_80_Reais_Deve_Retornar_1_Nota_de_50_Reais_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(80);

			Assert.AreEqual(3, notas.Count, "Quantidade de Notas");

			Assert.AreEqual(80, notas.Sum(n => n.Valor), "Valor do Saque");

			Assert.IsInstanceOfType(notas[0], typeof(Nota050), "Tipo da nota");
			Assert.IsInstanceOfType(notas[1], typeof(Nota020), "Tipo da nota");
			Assert.IsInstanceOfType(notas[2], typeof(Nota010), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_180_Reais_Deve_Retornar_1_Nota_de_100_Reais_1_Nota_de_50_Reais_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(180);

			Assert.AreEqual(4, notas.Count, "Quantidade de Notas");

			Assert.AreEqual(180, notas.Sum(n => n.Valor), "Valor do Saque");

			Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
			Assert.IsInstanceOfType(notas[1], typeof(Nota050), "Tipo da nota");
			Assert.IsInstanceOfType(notas[2], typeof(Nota020), "Tipo da nota");
			Assert.IsInstanceOfType(notas[3], typeof(Nota010), "Tipo da nota");
		}

		[TestMethod]
		public void Se_Sacar_190_Reais_Deve_Retornar_1_Nota_de_100_Reais_1_Nota_de_50_Reais_2_Notas_de_20_Reais_e_0_Notas_de_10_Reais()
		{
			var caixaForte = new CaixaForte();
			caixaForte.Inicializar();
			Saque saque = new Saque(caixaForte);

			IList<Nota> notas = saque.Sacar(190);

			Assert.AreEqual(4, notas.Count, "Quantidade de Notas");

			Assert.AreEqual(190, notas.Sum(n => n.Valor), "Valor do Saque");

			Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
			Assert.IsInstanceOfType(notas[1], typeof(Nota050), "Tipo da nota");
			Assert.IsInstanceOfType(notas[2], typeof(Nota020), "Tipo da nota");
			Assert.IsInstanceOfType(notas[3], typeof(Nota020), "Tipo da nota");

		}
	}


}