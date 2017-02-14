using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class PalavrasPrimasRunner : IExecutavel
	{
		public void Executar()
		{
			Console.Write("Informe uma palavra para saber se ela é prima ou não: ");
			var palavra = Console.ReadLine();
			var palavraPrima = new PalavrasPrimas();
			var resultado = palavraPrima.EhPrima(palavra);
			Console.WriteLine("A palavra {0} foi quantificada em {1} pontos e {2} uma palavra prima", palavra, palavraPrima.Quantificar(palavra), resultado ? "É" : "NÃO É");
		}
	}

	public class PalavrasPrimas
	{
		public Boolean EhPrima(String palavra)
		{
			if (Regex.IsMatch(palavra, "[a-zA-Z]"))
			{
				var valor = Quantificar(palavra);
				return EhPrimo(valor);
			}
			return false;
		}

		public Boolean EhPrimo(Int64 valor)
		{
			var ehPrimo = (valor == 2) || ((valor > 2) && (valor % 2 != 0));
			var maximo = Convert.ToInt64(Math.Sqrt(valor));

			for (var divisor = 3; ehPrimo && (divisor <= maximo); divisor = Next(divisor + 2))
			{
				ehPrimo = !EhDivisivel(valor, divisor);
			}

			return ehPrimo;
		}

		public Int32 Next(Int32 d)
		{
			var temp = 0;
			while (temp != d)
			{
				temp = d;
				if (d > 3 && (d % 3) == 0) d += 2;
				if (d > 5 && (d % 5) == 0) d += 2;
				if (d > 7 && (d % 7) == 0) d += 2;
				if (d > 11 && (d % 11) == 0) d += 2;
			}
			return d;
		}

		public Boolean EhDivisivel(Int64 numerador, Int64 denominador)
		{
			return (denominador != 0) && ((numerador % denominador) == 0);
		}


		public Int64 Quantificar(String str)
		{
			return str.Sum(c => Quantificar(c));
		}

		public Int64 Quantificar(Char chr)
		{
			if (Regex.IsMatch(chr.ToString(), "[a-z]"))
				return Convert.ToInt32(((byte)chr) - 96);
			else if (Regex.IsMatch(chr.ToString(), "[A-Z]"))
				return Convert.ToInt32(((byte)chr) - 38);
			else
				return 0;
		}
	}
}