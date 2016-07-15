using System;
using System.Collections.Generic;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Job
{
	public abstract class ContextoBase
	{
		public abstract List<MasterBase> Obter();
	}
	public abstract class MasterBase
	{
		public List<DetalheBase> Itens { get; set; }
	}

	public abstract class DetalheBase
	{
		public String Nome { get; set; }
		public DetalheBase()
		{
			Nome = "DetalheBase";
		}
	}
}