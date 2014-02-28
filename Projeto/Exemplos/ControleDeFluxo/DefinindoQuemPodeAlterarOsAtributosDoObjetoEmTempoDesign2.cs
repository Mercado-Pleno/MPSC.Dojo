using System;

namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	public class DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign2 : IExecutavel
	{
		public void Executar()
		{
			var dp = Funcionario.New("John Doe", 1500);
			dp.AumentarSalario(200);

			Funcionario funcionario = dp.Funcionario;

			Console.WriteLine("{0} - {1}", funcionario.Nome, funcionario.Salario);
		}
	}

	public interface IRecursosHumanos
	{
		void AumentarSalario(decimal valor);
		Funcionario Funcionario { get; }
	}

	public class Funcionario
	{
		public string Nome { get; private set; }
		public decimal Salario { get; protected set; }

		private Funcionario(string nome, decimal salario)
		{
			Nome = nome;
			Salario = salario;
		}

		public static IRecursosHumanos New(string name, decimal salario)
		{
			return new FuncionarioDP(name, salario);
		}

		private class FuncionarioDP : Funcionario, IRecursosHumanos
		{
			public FuncionarioDP(string nome, decimal salario) : base(nome, salario) { }

			public void AumentarSalario(decimal valor)
			{
				Salario += valor;
			}

			public Funcionario Funcionario
			{
				get { return this; }
			}
		}
	}
}