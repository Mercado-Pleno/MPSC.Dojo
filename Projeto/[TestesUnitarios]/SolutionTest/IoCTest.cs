using System;
using LBJC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace MP.Library.TestesUnitarios.SolutionTest
{
	[TestClass, TestFixture]
	public class IoCTest
	{
		[TestMethod, Test]
		public void Se_Tentar_Mapear_Uma_Classe_Para_Uma_Interface_Ja_Mapeada_Deve_Retornar_Uma_Excecao()
		{
			try
			{
				var IoC = new IoC();
				IoC.Map<IPessoa, Pessoa>()
					.Map<IAnimal, Animal>()
					.Map<IPessoa, Pessoa>();

				AssegureQue.EsteMetodoNaoDeveraSerExecutado();
			}
			catch (AssegureQue.ExecucaoDeCodigoProibidoException e)
			{
				throw new Exception("Teste Falhou!", e);
			}
			catch (Exception)
			{

			}
		}

		[TestMethod, Test]
		public void Se_Tentar_Mapear_Uma_Classe_Que_Nao_Implementa_A_Interface_Deve_Retornar_Uma_Excecao()
		{
			try
			{
				var IoC = new IoC();
				IoC.Map<IPessoa, Pessoa>()
					.Map<IAnimal, Animal>()
					.Map<IVeiculo, Carro>();

				AssegureQue.EsteMetodoNaoDeveraSerExecutado();
			}
			catch (AssegureQue.ExecucaoDeCodigoProibidoException e)
			{
				throw new Exception("Teste Falhou!", e);
			}
			catch (Exception)
			{

			}
		}

		[TestMethod, Test]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IPessoa_Deve_Retornar_Uma_Instancia_De_Pessoa()
		{
			var IoC = new IoC();
			IoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = IoC.New<IPessoa>();

			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IPessoa>(vObj);
			AssegureQue.EhDoTipo<Pessoa>(vObj);
		}

		[TestMethod, Test]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Deve_Retornar_Uma_Instancia_De_Animal()
		{
			var vIoC = CID.IoC
				.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = vIoC.New<IAnimal>();

			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IAnimal>(vObj);
			AssegureQue.EhDoTipo<Animal>(vObj);
		}

		[TestMethod, Test]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Com_Parametro_Deve_Retornar_Uma_Instancia_De_Animal_Com_Parametro()
		{
			var IoC = new IoC();
			IoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = CID.IoC.New<IAnimal>("Cachorro");

			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IAnimal>(vObj);
			AssegureQue.EhDoTipo<Animal>(vObj);
		}
	}


	public class AssegureQue
	{
		public class ExecucaoDeCodigoProibidoException : Exception { }

		public class TypeAccessException : TypeLoadException { }		

		public static void NaoEhNulo(Object obj)
		{
			if (obj == null)
				throw new NullReferenceException("obj");
		}

		public static void EhDoTipo<T1>(Object obj)
		{
			if (!(obj is T1))
				throw new TypeAccessException();
		}

		public static void EsteMetodoNaoDeveraSerExecutado()
		{
			throw new ExecucaoDeCodigoProibidoException();
		}
	}


}
