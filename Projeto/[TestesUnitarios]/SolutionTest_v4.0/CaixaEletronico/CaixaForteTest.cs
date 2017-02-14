using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico;
using MP.Library.CaixaEletronico.Notas;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass]
	public class CaixaForteTest
	{
		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_Deve_Retornar_0_Cedulas_Para_Nota_De_100()
		{
			var caixaForte = new CaixaForte();

			var quantidadeDeCedulas = caixaForte.ObterQuantidadeCedulasDe(new Nota100());

			Assert.AreEqual(0, quantidadeDeCedulas);
		}

		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_Deve_Retornar_0_Cedulas_Para_Nota_De_50()
		{
			var caixaForte = new CaixaForte();

			var quantidadeDeCedulas = caixaForte.ObterQuantidadeCedulasDe(new Nota050());

			Assert.AreEqual(0, quantidadeDeCedulas);
		}

		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_Deve_Retornar_0_Cedulas_Para_Nota_De_50_e_0CedulasParaNotaDe100()
		{
			var caixaForte = new CaixaForte();

			var quantidadeDeCedulas100 = caixaForte.ObterQuantidadeCedulasDe(new Nota100());
			Assert.AreEqual(0, quantidadeDeCedulas100);

			var quantidadeDeCedulas50 = caixaForte.ObterQuantidadeCedulasDe(new Nota050());
			Assert.AreEqual(0, quantidadeDeCedulas50);
		}

		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_E_informo_3_Notas_De100_Deve_Retornar_3_Cedulas_Para_Nota_De_100()
		{
			var caixaForte = new CaixaForte();

			caixaForte.InformarQuantidade(3, new Nota100());

			var quantidadeDeCedulas = caixaForte.ObterQuantidadeCedulasDe(new Nota100());

			Assert.AreEqual(3, quantidadeDeCedulas);
		}

		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_E_informo_5_Notas_De_50_Deve_Retornar_5_Cedulas_Para_Nota_De_50()
		{
			var caixaForte = new CaixaForte();

			caixaForte.InformarQuantidade(5, new Nota050());

			var quantidadeDeCedulas = caixaForte.ObterQuantidadeCedulasDe(new Nota050());

			Assert.AreEqual(5, quantidadeDeCedulas);
		}

		[TestMethod]
		public void Quando_Instancia_O_Servico_De_Cedulas_E_informo_5_Notas_De_50_e_3_Notas_De100_Deve_Retornar_5_Cedulas_Para_Nota_De_50_e3_Cedulas_Para_Nota_De_100()
		{
			var caixaForte = new CaixaForte();

			caixaForte.InformarQuantidade(3, new Nota100());
			caixaForte.InformarQuantidade(5, new Nota050());

			var quantidadeDeCedulas50 = caixaForte.ObterQuantidadeCedulasDe(new Nota050());
			Assert.AreEqual(5, quantidadeDeCedulas50);

			var quantidadeDeCedulas100 = caixaForte.ObterQuantidadeCedulasDe(new Nota100());
			Assert.AreEqual(3, quantidadeDeCedulas100);
		}
	}
}