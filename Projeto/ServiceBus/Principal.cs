namespace LBJC
{
	using System;

	public class Principal
	{
		public static void Main()
		{
		}
	}

	public interface IVeiculo { }
	public interface ICarro { }
	public interface IPessoa { }
	public interface IAnimal : IPessoa { /* String Nome { get; set; } */ }

	public class Teste { }
	public class Veiculo { }
	public class Bicicleta : Veiculo { }
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