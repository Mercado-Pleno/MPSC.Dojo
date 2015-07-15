using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest_v4.Cache
{
	[TestClass]
	public class TestandoContainer
	{
		[TestMethod]
		public void TestMethod1()
		{
			var containerObjetos = new ContainerDeObjetos();
			containerObjetos.Incluir(new ContainerCliente());
			containerObjetos.Incluir(new ContainerMensagem());


			var containerCliente = containerObjetos.Obter<ContainerCliente>();
			containerCliente.Incluir(new Cliente());
			var cliente = containerCliente.Obter(0);


			var containerMensagem = containerObjetos.Obter<ContainerMensagem>();
			containerMensagem.Incluir(Tipo.Nome, "Bruno");
			var nome = containerMensagem.Obter(Tipo.Nome);

		}


		public class ContainerCliente : ContainerAbstrato<Int64, Cliente>
		{
			protected override Cliente processarPesquisaExterna(Int64 key)
			{
				return new Cliente() { Id = key};
			}
		}

		public enum Tipo
		{
			Nome, Sobrenome
		}

		public class ContainerMensagem : ContainerAbstrato<Tipo, String>
		{
			protected override String processarPesquisaExterna(Tipo key)
			{
				return key.ToString();
			}
		}



		public class Cliente : IChaveUnica<Int64>
		{
			public Int64 Id { get; set; }

			public String Nome { get; set; }
		}


	}
}
