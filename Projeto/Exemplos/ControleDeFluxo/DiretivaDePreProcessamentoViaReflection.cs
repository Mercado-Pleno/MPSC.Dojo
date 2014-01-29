#define CondicaoEspecial

namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	using System;
	using System.Diagnostics;

	public class DiretivaDePreProcessamentoViaReflection : IExecutavel
	{
		public void Executar()
		{
			Console.WriteLine("Faça Alguma Coisa.");
			FazerAlgumaCoisa();
			Console.WriteLine("Fim de Alguma Coisa.");

			Console.WriteLine();

			Console.WriteLine("Faça Qualquer Coisa.");
			FazerQualquerCoisa();
			Console.WriteLine("Fim de Qualquer Coisa.");

			Console.WriteLine();

			Console.WriteLine("Faça Outra Coisa.");
			FazerOutraCoisa();
			Console.WriteLine("Fim de Outra Coisa.");
		}

		public void FazerAlgumaCoisa()
		{
			Console.WriteLine("Alguma Coisa Está Sendo Feita.");
		}

		[Conditional("CondicaoEspecial")]
		public void FazerQualquerCoisa()
		{
			Console.WriteLine("Qualquer Coisa Está Sendo Feita.");
		}

		public void FazerOutraCoisa()
		{
			Console.WriteLine("Outra Coisa Está Sendo Feita.");
		}
	}
}