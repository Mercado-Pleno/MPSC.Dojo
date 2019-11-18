using MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas;
using System;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class AvaliandoExpressoesMatematicasRunner : IExecutavel
	{
		public void Executar()
		{
			Console.Clear();
			Console.Write("\r\nInforme uma expressão matemática: ");
			var expressao = Console.ReadLine();
			var formula = new Formula() { OnResolver = (p, e) => Console.WriteLine($"{p} {e}") };
			var resultado = formula.Calcular(expressao);
			Console.WriteLine($"O resultado da expressão {expressao} é: {resultado}");
		}
	}
}
