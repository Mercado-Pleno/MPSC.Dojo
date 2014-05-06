using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.Aula.Curso;

namespace MP.Library.TestesUnitarios.SolutionTest.Curso
{
	[TestClass]
	public class NumeroRomanoTest
	{
		[TestMethod]
		public void Se_Instanciar_O_Numero_1_Deve_Retornar_I_eo_Valor_Deve_Ser_1()
		{
			var numeroRomano = new Numero1();
			Assert.AreEqual(1, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("I", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_2_Deve_Retornar_II_eo_Valor_Deve_Ser_2()
		{
			var numeroRomano = new Numero2();
			Assert.AreEqual(2, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("II", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_3_Deve_Retornar_III_eo_Valor_Deve_Ser_3()
		{
			var numeroRomano = new Numero3();
			Assert.AreEqual(3, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("III", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_4_Deve_Retornar_IV_eo_Valor_Deve_Ser_4()
		{
			var numeroRomano = new Numero4();
			Assert.AreEqual(4, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("IV", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_5_Deve_Retornar_V_eo_Valor_Deve_Ser_5()
		{
			var numeroRomano = new Numero5();
			Assert.AreEqual(5, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("V", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_6_Deve_Retornar_VI_eo_Valor_Deve_Ser_6()
		{
			var numeroRomano = new Numero6();
			Assert.AreEqual(6, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("VI", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_7_Deve_Retornar_VII_eo_Valor_Deve_Ser_7()
		{
			var numeroRomano = new Numero7();
			Assert.AreEqual(7, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("VII", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_8_Deve_Retornar_VIII_eo_Valor_Deve_Ser_8()
		{
			var numeroRomano = new Numero8();
			Assert.AreEqual(8, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("VIII", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_9_Deve_Retornar_IX_eo_Valor_Deve_Ser_9()
		{
			var numeroRomano = new Numero9();
			Assert.AreEqual(9, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("IX", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_10_Deve_Retornar_X_eo_Valor_Deve_Ser_10()
		{
			var numeroRomano = new Numero10();
			Assert.AreEqual(10, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("X", numeroRomano.Caracter, "Caractere Inválido");
		}


		[TestMethod]
		public void Se_Instanciar_O_Numero_20_Deve_Retornar_XX_eo_Valor_Deve_Ser_20()
		{
			var numeroRomano = new Numero20();
			Assert.AreEqual(20, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("XX", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_19_Deve_Retornar_XIX_eo_Valor_Deve_Ser_19()
		{
			var numeroRomano = new Numero19();
			Assert.AreEqual(19, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("XIX", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_49_Deve_Retornar_XLIX_eo_Valor_Deve_Ser_49()
		{
			var numeroRomano = new Numero49();
			Assert.AreEqual(49, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("XLIX", numeroRomano.Caracter, "Caractere Inválido");
		}



	}
}