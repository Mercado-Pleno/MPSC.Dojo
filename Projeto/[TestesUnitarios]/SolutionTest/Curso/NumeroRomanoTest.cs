using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.Aula.Curso;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace MP.Library.TestesUnitarios.SolutionTest.Curso
{
	[TestClass, TestFixture]
	public class NumeroRomanoTest
	{
		[TestMethod, Test]
		public void SeInstanciarONumero1DeveRetornarIeoValorDeveSer1()
		{
			var numeroRomano = new Numero1();
			Assert.AreEqual(1, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("I", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod, Test]
		public void SeInstanciarONumero2DeveRetornarIIeoValorDeveSer2()
		{
			var numeroRomano = new Numero2();
			Assert.AreEqual(2, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("II", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod, Test]
		public void SeInstanciarONumero3DeveRetornarIIIeoValorDeveSer3()
		{
			var numeroRomano = new Numero3();
			Assert.AreEqual(3, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("III", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod, Test]
		public void SeInstanciarONumero4DeveRetornarIVeoValorDeveSer4()
		{
			var numeroRomano = new Numero4();
			Assert.AreEqual(4, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("IV", numeroRomano.Caracter, "Caractere Inválido");
		}

		[TestMethod, Test]
		public void SeInstanciarONumero5DeveRetornarVeoValorDeveSer5()
		{
			var numeroRomano = new Numero5();
			Assert.AreEqual(5, numeroRomano.Valor, "Valor Inválido");
			Assert.AreEqual("V", numeroRomano.Caracter, "Caractere Inválido");
		}
	}
}