﻿using System;
using LBJC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MP.Library.TestesUnitarios.SolutionTest
{
	[TestClass]
	public class IoCTest
	{
		[TestMethod]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IPessoa_Deve_Retornar_Uma_Instancia_De_Pessoa()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = vIoC.New<IPessoa>();

			Assert.IsNotNull(vObj);
			Assert.IsInstanceOfType(vObj, typeof(IPessoa));
			Assert.IsInstanceOfType(vObj, typeof(Pessoa));
		}

		[TestMethod]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Sem_Parametro_No_Mapeamento_E_Sem_Parametro_No_New_Deve_Retornar_Uma_Instancia_De_Animal_Com_Nome_Nulo()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = vIoC.New<IAnimal>();

			Assert.IsNotNull(vObj);
			Assert.IsInstanceOfType(vObj, typeof(IAnimal));
			Assert.IsInstanceOfType(vObj, typeof(Animal));
			Assert.AreEqual(null, vObj.Nome);
		}

		[TestMethod]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Com_Parametro_No_Mapeamento_E_Sem_Parametro_No_New_Deve_Retornar_Uma_Instancia_De_Animal_Com_Nome_Animal()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>("Animal");

			var vObj = vIoC.New<IAnimal>();

			Assert.IsNotNull(vObj);
			Assert.IsInstanceOfType(vObj, typeof(IAnimal));
			Assert.IsInstanceOfType(vObj, typeof(Animal));
			Assert.AreEqual("Animal", vObj.Nome);
		}

		[TestMethod]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Com_Parametro_No_Mapeamento_E_Com_Parametro_No_New_Deve_Retornar_Uma_Instancia_De_Animal_Com_Nome_Cachorro()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>("Animal");

			var vObj = vIoC.New<IAnimal>("Cachorro");

			Assert.IsNotNull(vObj);
			Assert.IsInstanceOfType(vObj, typeof(IAnimal));
			Assert.IsInstanceOfType(vObj, typeof(Animal));
			Assert.AreEqual("Cachorro", vObj.Nome);
		}

		[TestMethod]
		public void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Sem_Parametro_No_Mapeamento_E_Com_Parametro_No_New_Deve_Retornar_Uma_Instancia_De_Animal_Com_Nome_Cachorro()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>();

			var vObj = vIoC.New<IAnimal>("Cachorro");

			Assert.IsNotNull(vObj);
			Assert.IsInstanceOfType(vObj, typeof(IAnimal));
			Assert.IsInstanceOfType(vObj, typeof(Animal));
			Assert.AreEqual("Cachorro", vObj.Nome);
		}

		[TestMethod]
		public void Se_Tentar_Instanciar_Uma_Classe_Que_Nao_Esteja_Mapeada_e_Se_Ignora_o_Erro_Deve_Retornar_Uma_Instancia()
		{
			var vAgora = DateTime.Now;
			var vIoC = new IoC(true);
			var vObj = vIoC.New<DateTime>(vAgora.Ticks);

			Assert.IsNotNull(vObj);
			Assert.AreEqual(vAgora, vObj);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException), "Precisava disparar Exceção, mas NÃO disparou!")]
		public void Se_Tentar_Instanciar_Uma_Classe_Que_Nao_Esteja_Mapeada_e_Se_Nao_Ignora_o_Erro_Deve_Retornar_Uma_Exception()
		{
			var vAgora = DateTime.Now;
			var vIoC = new IoC(false);
			var vObj = vIoC.New<DateTime>(vAgora.Ticks);

			Assert.IsNotNull(vObj);
			Assert.AreEqual(vAgora, vObj);
			Assert.Fail("Precisava disparar Exceção, mas NÃO disparou!");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException), "Precisava disparar Exceção, mas NÃO disparou!")]
		public void Se_Tentar_Instanciar_Uma_Classe_Que_Nao_Esteja_Mapeada_e_Se_o_Parametro_De_Ignora_o_Erro_For_Omitido_Deve_Retornar_Uma_Exception()
		{
			var vAgora = DateTime.Now;
			var vIoC = new IoC();
			var vObj = vIoC.New<DateTime>(vAgora.Ticks);

			Assert.IsNotNull(vObj);
			Assert.AreEqual(vAgora, vObj);
			Assert.Fail("Precisava disparar Exceção, mas NÃO disparou!");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException), "Precisava disparar Exceção, mas NÃO disparou!")]
		public void Se_Tentar_Mapear_Uma_Classe_Para_Uma_Interface_Ja_Mapeada_Deve_Retornar_Uma_Excecao()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>()
				.Map<IPessoa, Pessoa>();

			Assert.Fail("Precisava disparar Exceção, mas NÃO disparou!");
		}

		[TestMethod, ExpectedException(typeof(ArgumentException), "Precisava disparar Exceção, mas NÃO disparou!")]
		public void Se_Tentar_Mapear_Uma_Classe_Que_Nao_Implementa_A_Interface_Deve_Retornar_Uma_Excecao()
		{
			var vIoC = new IoC(false);
			vIoC.Map<IVeiculo, Veiculo>();

			Assert.Fail("Precisava disparar Exceção, mas NÃO disparou!");
		}
	}
}