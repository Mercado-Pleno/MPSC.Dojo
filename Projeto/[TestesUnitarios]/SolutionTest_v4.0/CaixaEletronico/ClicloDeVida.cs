using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.CaixaEletronico
{
	[TestClass]
	public class ClicloDeVida
	{
		[ClassInitialize]
		public static void InicializandoClasseDeTeste(TestContext contexto)
		{
			Console.WriteLine("InicializandoClasseDeTeste");
		}

		[TestInitialize]
		public void InicializandoMetodoDeTeste()
		{
			Console.WriteLine("InicializandoMetodoDeTeste");
		}

		[TestMethod]
		public void Teste1()
		{
			Console.WriteLine("Teste1()");
		}

		[TestMethod]
		public void Teste2()
		{
			Console.WriteLine("Teste2()");
		}

		[TestMethod]
		public void Teste3()
		{
			Console.WriteLine("Teste3()");
		}

		[TestCleanup]
		public void FinalizandoMetodoDeTeste()
		{
			Console.WriteLine("FinalizandoMetodoDeTeste");
		}

		[ClassCleanup]
		public static void FinalizandoClasseDeTeste()
		{
			Console.WriteLine("FinalizandoClasseDeTeste");
		}
	}
}