using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public static class OperacaoFactory
	{
		public static readonly char[] Tokens = new[] { '(', '[', '{', '*', '/', '^', '%', '+', '-', ' ', '}', ']', ')' };
		public static readonly string[] StringTokens = Tokens.Select(t => t.ToString()).ToArray();
		private static readonly IDictionary<char, Operacao> _operacoes = CarregarOperacoesDisponiveis();

		private static IDictionary<char, Operacao> CarregarOperacoesDisponiveis()
		{
			return new Dictionary<char, Operacao>
			{
				{ '*', new Multiplicar() },
				{ '/', new Dividir() },
				{ '^', new Potencia() },
				{ '%', new Modulo() },
				{ '+', new Somar() },
				{ '-', new Subtrair() },
			};
		}

		public static Operacao ObterPor(string operador)
		{
			return ObterPor(operador.FirstOrDefault());
		}

		public static Operacao ObterPor(char operador)
		{
			return _operacoes.TryGetValue(operador, out var value) ? value : null;
		}
	}

	public class Multiplicar : Operacao
	{
		public Multiplicar() : base((n1, n2) => n1 * n2) { }
	}

	public class Dividir : Operacao
	{
		public Dividir() : base((n1, n2) => n1 / n2) { }
	}

	public class Potencia : Operacao
	{
		public Potencia() : base((n1, n2) => Convert.ToDecimal(Math.Pow(Convert.ToDouble(n1), Convert.ToDouble(n2)))) { }
	}

	public class Modulo : Operacao
	{
		public Modulo() : base((n1, n2) => n1 % n2) { }
	}

	public class Somar : Operacao
	{
		public Somar() : base((n1, n2) => n1 + n2) { }
	}

	public class Subtrair : Operacao
	{
		public Subtrair() : base((n1, n2) => n1 - n2) { }
	}
}