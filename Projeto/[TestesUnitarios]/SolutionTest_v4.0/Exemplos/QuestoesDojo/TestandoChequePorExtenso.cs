using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoChequePorExtenso
	{
		[TestMethod]
		public void TestMethod1()
		{
			var cheque = ChequePorExtenso.Novo(253);

			Assert.AreEqual("duzentos e cinquenta e tres", cheque.Descricao);
		}
	}
}