using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoAutorDeObraBibliografica
	{
		[TestMethod]
		public void QuandoTestaDividirEmNomes()
		{
			var autorDeObraBibliografica = new AutorDeObraBibliografica();

			Assert.AreEqual(0, autorDeObraBibliografica.DividirEmNomes("").Length);
			Assert.AreEqual(3, autorDeObraBibliografica.DividirEmNomes("Bruno Nogueira Fernandes").Length);
		}

		[TestMethod]
		public void QuandoTestaPrimeirosNomes()
		{
			var autorDeObraBibliografica = new AutorDeObraBibliografica();

			Assert.AreEqual("", autorDeObraBibliografica.PrimeirosNomes(""));
			Assert.AreEqual("Bruno Nogueira", autorDeObraBibliografica.PrimeirosNomes("Bruno Nogueira Fernandes"));
			Assert.AreEqual("", autorDeObraBibliografica.PrimeirosNomes("Fernandes"));
			Assert.AreEqual("Antonio", autorDeObraBibliografica.PrimeirosNomes("Antonio Neto"));
			Assert.AreEqual("Antonio José", autorDeObraBibliografica.PrimeirosNomes("Antonio José Fernandes Neto"));
		}

		[TestMethod]
		public void QuandoTestaSobreNome()
		{
			var autorDeObraBibliografica = new AutorDeObraBibliografica();

			Assert.AreEqual("", autorDeObraBibliografica.SobreNome(""));
			Assert.AreEqual("Fernandes", autorDeObraBibliografica.SobreNome("Bruno Nogueira Fernandes"));
			Assert.AreEqual("Fernandes", autorDeObraBibliografica.SobreNome("Fernandes"));
			Assert.AreEqual("Neto", autorDeObraBibliografica.SobreNome("Antonio Neto"));
			Assert.AreEqual("Fernandes Neto", autorDeObraBibliografica.SobreNome("Antonio José Fernandes Neto"));
		}

		[TestMethod]
		public void QuandoTestaFormatar()
		{
			var autorDeObraBibliografica = new AutorDeObraBibliografica();

			Assert.AreEqual("FERNANDES NETO, Antonio José", autorDeObraBibliografica.Formatar("Antonio José Fernandes Neto"));
			Assert.AreEqual("FERNANDES NETO, Antonio José", autorDeObraBibliografica.Formatar("ANTONIO JOSÉ FERNANDES NETO"));
			Assert.AreEqual("FERNANDES NETO, Antonio José", autorDeObraBibliografica.Formatar("antonio josé fernandes neto"));

			Assert.AreEqual("FERNANDES NETO, Antonio José de", autorDeObraBibliografica.Formatar("Antonio José de Fernandes Neto"));
			Assert.AreEqual("FERNANDES NETO, Antonio José de", autorDeObraBibliografica.Formatar("ANTONIO JOSÉ DE FERNANDES NETO"));
			Assert.AreEqual("FERNANDES NETO, Antonio José de", autorDeObraBibliografica.Formatar("antonio josé de fernandes neto"));

			Assert.AreEqual("", autorDeObraBibliografica.Formatar(""));
			Assert.AreEqual("FERNANDES", autorDeObraBibliografica.Formatar("Fernandes"));
			Assert.AreEqual("FERNANDES, Antonio", autorDeObraBibliografica.Formatar("Antonio Fernandes"));
			Assert.AreEqual("NETO, Antonio", autorDeObraBibliografica.Formatar("Antonio Neto"));
			Assert.AreEqual("FERNANDES NETO, Antonio", autorDeObraBibliografica.Formatar("Antonio Fernandes Neto"));
			Assert.AreEqual("FERNANDES, Antonio José", autorDeObraBibliografica.Formatar("Antonio José Fernandes"));
			Assert.AreEqual("FERNANDES NETO, Antonio José", autorDeObraBibliografica.Formatar("Antonio José Fernandes Neto"));
		}
	}
}