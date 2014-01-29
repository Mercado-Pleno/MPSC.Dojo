namespace LBJC
{
	using System;

	public class Principal
	{
		public static void Main()
		{
			CID.IoC
				.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>()
				;
			Se_Tentar_Mapear_Uma_Classe_Para_Uma_Interface_Ja_Mapeada_Deve_Retornar_Uma_Excecao();
			Se_Tentar_Mapear_Uma_Classe_Que_Nao_Implementa_A_Interface_Deve_Retornar_Uma_Excecao();
			Se_Solicitar_Uma_Clase_Que_Implemente_IPessoa_Deve_Retornar_Uma_Instancia_De_Pessoa();
			Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Deve_Retornar_Uma_Instancia_De_Animal();
			Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Com_Parametro_Deve_Retornar_Uma_Instancia_De_Animal_Com_Parametro();
		}

		private static void Se_Tentar_Mapear_Uma_Classe_Para_Uma_Interface_Ja_Mapeada_Deve_Retornar_Uma_Excecao()
		{
			try
			{
				CID.IoC.Map<IPessoa, Pessoa>();
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

		private static void Se_Tentar_Mapear_Uma_Classe_Que_Nao_Implementa_A_Interface_Deve_Retornar_Uma_Excecao()
		{
			try
			{
				CID.IoC.Map<IVeiculo, Carro>();
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

		private static void Se_Solicitar_Uma_Clase_Que_Implemente_IPessoa_Deve_Retornar_Uma_Instancia_De_Pessoa()
		{
			var vObj = CID.IoC.New<IPessoa>();
			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IPessoa>(vObj);
			AssegureQue.EhDoTipo<Pessoa>(vObj);
		}

		private static void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Deve_Retornar_Uma_Instancia_De_Animal()
		{
			var vObj = CID.IoC.New<IAnimal>();
			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IAnimal>(vObj);
			AssegureQue.EhDoTipo<Animal>(vObj);
		}

		private static void Se_Solicitar_Uma_Clase_Que_Implemente_IAnimal_Com_Parametro_Deve_Retornar_Uma_Instancia_De_Animal_Com_Parametro()
		{
			var vObj = CID.IoC.New<IAnimal>("Cachorro");
			AssegureQue.NaoEhNulo(vObj);
			AssegureQue.EhDoTipo<IAnimal>(vObj);
			AssegureQue.EhDoTipo<Animal>(vObj);
		}

	}


	public class AssegureQue
	{
		public class ExecucaoDeCodigoProibidoException : Exception { }

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

	public interface IVeiculo { }
	public interface ICarro { }
	public interface IPessoa { }
	public interface IAnimal : IPessoa { /* String Nome { get; set; } */ }

	public class Veiculo : IVeiculo { }
	public class Carro : ICarro { }
	public class Pessoa : IPessoa { }
	public class Animal : IAnimal
	{
		public String Nome { get; set; }
		public Animal() { }
		public Animal(int a, int b) { }
		public Animal(String nome)
		{
			Nome = nome;
		}
	}
}