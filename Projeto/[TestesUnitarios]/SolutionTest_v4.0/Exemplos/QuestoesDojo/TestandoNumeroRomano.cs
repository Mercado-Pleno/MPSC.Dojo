using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoNumeroRomano
	{
		[TestMethod]
		public void Se_Instanciar_O_Numero_1_Deve_Retornar_I_eo_Valor_Deve_Ser_1()
		{
			var numeroRomano = NumeroRomano.Novo(1);
			Assert.AreEqual(1, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("I", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_2_Deve_Retornar_II_eo_Valor_Deve_Ser_2()
		{
			var numeroRomano = NumeroRomano.Novo(2);
			Assert.AreEqual(2, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("II", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_3_Deve_Retornar_III_eo_Valor_Deve_Ser_3()
		{
			var numeroRomano = NumeroRomano.Novo(3);
			Assert.AreEqual(3, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("III", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_4_Deve_Retornar_IV_eo_Valor_Deve_Ser_4()
		{
			var numeroRomano = NumeroRomano.Novo(4);
			Assert.AreEqual(4, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("IV", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_5_Deve_Retornar_V_eo_Valor_Deve_Ser_5()
		{
			var numeroRomano = NumeroRomano.Novo(5);
			Assert.AreEqual(5, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("V", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_6_Deve_Retornar_VI_eo_Valor_Deve_Ser_6()
		{
			var numeroRomano = NumeroRomano.Novo(6);
			Assert.AreEqual(6, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("VI", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_7_Deve_Retornar_VII_eo_Valor_Deve_Ser_7()
		{
			var numeroRomano = NumeroRomano.Novo(7);
			Assert.AreEqual(7, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("VII", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_8_Deve_Retornar_VIII_eo_Valor_Deve_Ser_8()
		{
			var numeroRomano = NumeroRomano.Novo(8);
			Assert.AreEqual(8, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("VIII", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_9_Deve_Retornar_IX_eo_Valor_Deve_Ser_9()
		{
			var numeroRomano = NumeroRomano.Novo(9);
			Assert.AreEqual(9, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("IX", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_10_Deve_Retornar_X_eo_Valor_Deve_Ser_10()
		{
			var numeroRomano = NumeroRomano.Novo(10);
			Assert.AreEqual(10, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("X", numeroRomano.Representacao, "Representação Inválida");
		}


		[TestMethod]
		public void Se_Instanciar_O_Numero_20_Deve_Retornar_XX_eo_Valor_Deve_Ser_20()
		{
			var numeroRomano = NumeroRomano.Novo(20);
			Assert.AreEqual(20, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("XX", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_19_Deve_Retornar_XIX_eo_Valor_Deve_Ser_19()
		{
			var numeroRomano = NumeroRomano.Novo(19);
			Assert.AreEqual(19, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("XIX", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_49_Deve_Retornar_XLIX_eo_Valor_Deve_Ser_49()
		{
			var numeroRomano = NumeroRomano.Novo(49);
			Assert.AreEqual(49, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("XLIX", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_4880_Deve_Retornar_ivDCCCLXXX_eo_Valor_Deve_Ser_4880()
		{
			var numeroRomano = NumeroRomano.Novo(4880);
			Assert.AreEqual(4880, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("ivDCCCLXXX", numeroRomano.Representacao, "Representação Inválida");
		}

		[TestMethod]
		public void Se_Instanciar_O_Numero_4884_Deve_Retornar_ivDCCCLXXXIV_eo_Valor_Deve_Ser_4884()
		{
			var numeroRomano = NumeroRomano.Novo(4884);
			Assert.AreEqual(4884, numeroRomano.ValorSemantico, "Valor Semântico Inválido");
			Assert.AreEqual("ivDCCCLXXXIV", numeroRomano.Representacao, "Representação Inválida");
		}
	}
}