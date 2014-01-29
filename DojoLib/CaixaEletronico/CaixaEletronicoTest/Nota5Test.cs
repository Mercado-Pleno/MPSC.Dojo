using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject1
{
	[TestClass()]
	public class Nota5Test
	{
		[TestMethod]
		public void Se_a_Nota_For_Uma_Nota_de_5_Reais_o_Valor_da_Nota_Deve_Ser_5()
		{
			Nota nota = new Nota5();

			Assert.AreEqual(5, nota.Valor, "Valor da Nota"); 
		}
	}
}