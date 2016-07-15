using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Job;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Job
{
	[TestClass]
	public class TestandoJobBase
	{
		[TestMethod]
		public void TestMethod1()
		{
			var job = new JobBase<CalculadoraDeRaizQuadrada>();
			job.Executar();
		}
	}
}