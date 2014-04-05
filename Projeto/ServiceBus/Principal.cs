﻿namespace LBJC
{
	using System;

	public class Principal
	{
		public static void Main()
		{
		}
	}

	public interface IAnimal { String Nome { get; } }
	public interface IPessoa { }
	public interface IVeiculo { }

	public class Animal : IAnimal
	{
		public String Nome { get; private set; }
		public Animal() { }
		public Animal(String nome) { Nome = nome; }
	}

	public class Pessoa : IPessoa { }

	public class Bicicleta : Veiculo, IVeiculo { }
	public class Carro : Veiculo, IVeiculo { }
	public class Veiculo { }
}