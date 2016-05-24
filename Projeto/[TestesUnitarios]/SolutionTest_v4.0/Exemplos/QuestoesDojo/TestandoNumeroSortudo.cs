using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoNumeroSortudo
	{
		[TestMethod]
		public void QuandoPerguntaSeNumerosEhSortudo()
		{
			var numeroSortudo = new NumeroSortudo();
			Assert.IsTrue(numeroSortudo.EhSortudo(15, 0));
			Assert.IsTrue(numeroSortudo.EhSortudo(15, 1));
			Assert.IsTrue(numeroSortudo.EhSortudo(15, 2));
			Assert.IsFalse(numeroSortudo.EhSortudo(15, 3));
			Assert.IsFalse(numeroSortudo.EhSortudo(15, 4));
			Assert.IsFalse(numeroSortudo.EhSortudo(15, 100));
		}
	}
}