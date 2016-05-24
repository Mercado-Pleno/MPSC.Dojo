using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class NumeroFeliz
	{
		public Boolean EhFeliz(Int64 numero, Int32 iteracoes)
		{
			return IteracoesDeFelicidade(numero, iteracoes) >= 0;
		}

		public Int32 IteracoesDeFelicidade(Int64 numero, Int32 iteracoes)
		{
			if ((numero == 1) && (iteracoes >= 0))
				return 0;
			else if ((numero > 1) && (iteracoes > 0))
			{
				var digitos = ObterDigitos(numero);
				var novoNumero = digitos.Sum(digito => digito * digito);
				var ehFeliz = IteracoesDeFelicidade(novoNumero, iteracoes - 1);
				return ehFeliz + (ehFeliz >= 0 ? 1 : 0);
			}

			return -1;
		}

		public IEnumerable<Int64> ObterDigitos(Int64 numero)
		{
			if (numero == 0L)
				yield return numero;
			else
			{
				while (numero > 0L)
				{
					yield return(numero % 10L);
					numero = numero / 10L;
				}
			}
		}
	}
}