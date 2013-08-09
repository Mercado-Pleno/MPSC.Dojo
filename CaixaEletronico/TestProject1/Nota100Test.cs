using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
	[TestClass()]
	public class Nota100Test
	{
		[TestMethod]
		public void Se_a_Nota_For_Uma_Nota_de_100_Reais_o_Valor_da_Nota_Deve_Ser_100()
		{
			Nota nota = new Nota100();

			Assert.AreEqual(100, nota.Valor, "Valor da Nota"); 
		}
	}
}