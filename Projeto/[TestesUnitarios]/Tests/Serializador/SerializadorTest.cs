using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System;

namespace MP.LBJC.Tests
{
    [TestFixture]
	public class SerializadorTest
    {
		private int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		private List<Venda> vendas;
        [TestFixtureSetUp]
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

		[Test]
		public void GetMensagemENomeBase()
		{
			var g = numeros.GroupBY(n => n % 2, n => n > 5);
			Assert.AreEqual(4, g.Count);
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