using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = NUnit.Framework.Assert;

namespace MP.LBJC.Tests
{
	[TestFixture, TestClass]
	public class SerializadorTest
	{
		private int[] numeros = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		private List<Venda> vendas;

		[TestFixtureSetUp, TestInitialize]
		public void SetUp()
		{
			vendas = new List<Venda>();
			vendas.Add(new Venda("Jan", "Mouse", 1));
			vendas.Add(new Venda("Jan", "Teclado", 1));
			vendas.Add(new Venda("Jan", "Monitor", 1));
			vendas.Add(new Venda("Fev", "Mouse", 1));
			vendas.Add(new Venda("Fev", "Teclado", 1));
			vendas.Add(new Venda("Fev", "Monitor", 1));
		}

		[Test, TestMethod]
		public void Agrupar_ParImpar_MenorMaior()
		{
			var g = numeros.GroupBY(n => n % 2, n => n >= 5);
			Assert.AreEqual(4, g.Count);
		}

        [Test, TestMethod]
		public void Agrupar_ParImpar()
		{
			var g = numeros.GroupBY(n => n % 2);
			Assert.AreEqual(2, g.Count);
		}

        [Test, TestMethod]
		public void Agrupar_MenorMaior()
		{
			var g = numeros.GroupBY(n => n >= 5);
			Assert.AreEqual(2, g.Count);
		}

        [Test, TestMethod]
		public void Agrupar_Mes()
		{
			var g = vendas.GroupBY(v => v.Mes);
			Assert.AreEqual(2, g.Count);
		}

        [Test, TestMethod]
		public void Agrupar_Produto()
		{
			var g = vendas.GroupBY(v => v.Produto);
			Assert.AreEqual(3, g.Count);
		}
	}

	public class Venda
	{
		public String Mes { get; set; }
		public String Produto { get; set; }
		public int Quantidade { get; set; }

		public Venda(string mes, string produto, int quantidade)
		{
			Mes = mes;
			Produto = produto;
			Quantidade = quantidade;
		}
	}
}