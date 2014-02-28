using System;

namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	public class DefinindoQuemPodeAlterarOsAtributosDoObjetoEmTempoDesign1 : IExecutavel
	{
		public void Executar()
		{
			var dp = Empregado.New(name: "John Doe", salario: 1500);
			dp.AumentarSalario(200);

			var empregado = dp.Empregado;
			Console.WriteLine("{0} - {1}", empregado.Nome, empregado.Salario);
		}
	}

	interface IDepartamentoPessoal
	{
		Empregado Empregado { get; }
		void AumentarSalario(decimal valor);
	}

	class Empregado
	{
		public string Nome { get; private set; }
		public decimal Salario { get; private set; }

		private Empregado(string nome, decimal salario)
		{
			Nome = nome;
			Salario = salario;
		}

		public static IDepartamentoPessoal New(string name, decimal salario)
		{
			var empregado = new Empregado(name, salario);
			return new DepartamentoPessoal(empregado);
		}

		private class DepartamentoPessoal : IDepartamentoPessoal
		{
			private readonly Empregado _empregado;
			public DepartamentoPessoal(Empregado empregado)
			{
				_empregado = empregado;
			}

			public Empregado Empregado
			{
				get { return _empregado; }
			}

			public void AumentarSalario(decimal valor)
			{
				_empregado.Salario += valor;
			}
		}
	}
}