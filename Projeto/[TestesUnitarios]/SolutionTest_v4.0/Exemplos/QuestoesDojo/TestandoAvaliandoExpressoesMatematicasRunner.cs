using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas;
using System;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoAvaliandoExpressoesMatematicasRunner
	{
		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_01()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "3 *  7 ");
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "3 * ( 7 )");
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "3 * ( 2 + 5 )");
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "3 * ( 2 + 3 + 2 )");
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "3 * ( 2 + 2 + 1 + 2 )");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_02()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(36M, "3 * (2 + 5 * 2)");
			CalcularUmaExpressaoVerificandoValorDeRetorno(36M, "3 * (2 * 5 + 2)");
			CalcularUmaExpressaoVerificandoValorDeRetorno(36M, "(2 + 5 * 2) * 3");
			CalcularUmaExpressaoVerificandoValorDeRetorno(36M, "(2 * 5 + 2) * 3");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_03()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(42M, "3 * ((2 + 5) * 2)");
			CalcularUmaExpressaoVerificandoValorDeRetorno(42M, "3 * (2 * (2 + 5))");
			CalcularUmaExpressaoVerificandoValorDeRetorno(42M, "3 * (2 * (2 *1 + 5 * 1))");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_04()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(21M, "( 3 * ( 2 + 5 ) )");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_05()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(36M, "(3 * (2 + 5 * 2))");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularUmaExpressao_DeveRetornarOValorCorreto_06()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(42M, "(3 * ((2 + (1+2*2)) * 2))");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularExpressao5ElevadoA4aPotencia_DeveRetornar625()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(625M, "5 ^ 4");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularExpressao5Modulo2_DeveRetornar1()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(1M, "5 % 2");
		}

		[TestMethod]
		public void QuandoPedeParaFormulaCalcularExpressao2Menos5_DeveRetornarMenos3()
		{
			CalcularUmaExpressaoVerificandoValorDeRetorno(-3M, "2 - 5");
			CalcularUmaExpressaoVerificandoValorDeRetorno(-3M, "- 5 + 2");
			CalcularUmaExpressaoVerificandoValorDeRetorno(+7M, "-- 5 + 2");
			CalcularUmaExpressaoVerificandoValorDeRetorno(-3M, "--- 5 + 2");
			CalcularUmaExpressaoVerificandoValorDeRetorno(+7M, "2 - -5");
			CalcularUmaExpressaoVerificandoValorDeRetorno(-7M, "- 5 + -2");
		}

		private void CalcularUmaExpressaoVerificandoValorDeRetorno(decimal valorEsperado, string expressao)
		{
			try
			{
				var formula = new Formula() { OnResolver = (p, e) => Console.WriteLine($"{p} {e}") };
				var valorCalculado = formula.Calcular(expressao);
				Assert.AreEqual(valorEsperado, valorCalculado, expressao);
			}
			catch (Exception exception)
			{
				throw new Exception(expressao, exception);
			}
		}
	}
}