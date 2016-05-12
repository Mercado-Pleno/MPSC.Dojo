using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class PalavrasPrimas
	{

		public Boolean EhUmaPalavraPrima(String palavra)
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
			var EhPrimo = valor >= 2;
			var maximo = Convert.ToInt64(Math.Sqrt(valor));

			for (Int64 divisor = 2; EhPrimo && (divisor <= maximo); divisor++)
			{
				EhPrimo = !EhDivisivel(valor, divisor);
			}

			return EhPrimo;
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