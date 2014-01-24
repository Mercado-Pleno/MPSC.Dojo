namespace LBJC
{
	using System;
	using ServiceBus;

	public class Principal
	{
		public static void Main()
		{
			Container.IoC
				.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>()
				;

			//var vObj1 = Container.IoC.New<IPessoa>();
			//var vObj2 = Container.IoC.New<IAnimal>();
			//var vObj3 = Container.IoC.New<IAnimal>("Cachorro");

		}
	}


	public interface IPessoa
	{

	}

	public class Pessoa : IPessoa
	{

	}

	public interface IAnimal : IPessoa
	{
		//String Nome { get; set; }
	}

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