using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Office
{
	[TestClass]
	public class ExcelTest
	{
		[TestInitialize()]
		public void MyTestInitialize() { }

		[TestCleanup()]
		public void MyTestCleanup() { }


		[TestMethod]
		public void TestMethod1()
		{
			var planilhaDoExcel = new Planilha(@"D:\Relatório.xlsx");

			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(1, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(1, 2), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(2, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(2, 2), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(1, 3), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
			planilhaDoExcel.AdicionarDados("Plan1", Celula.From(3, 1), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

			planilhaDoExcel.AdicionarDados("Plan1", Celula.From("D1"), DateTime.Now.ToString("dd/MM/yyyy"));
			planilhaDoExcel.AdicionarDados("Plan1", Celula.From("D1"), DateTime.Now.ToString("HH:mm:ss.fff"));

			planilhaDoExcel.Gravar();
		}
	}


}
