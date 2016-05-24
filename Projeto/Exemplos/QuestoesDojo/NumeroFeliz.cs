using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class NumeroFeliz
	{
		public Boolean EhFeliz(Int64 numero, Int32 iteracoes)
		{
			return PosicaoDaFelicidade(numero, iteracoes) >= 0;
		}

		public Int32 PosicaoDaFelicidade(Int64 numero, Int32 iteracoes)
		{
			var retorno = -1;
			if ((numero == 1) && (iteracoes >= 0))
				retorno = 0;
			else if ((numero > 1) && (iteracoes > 0))
			{
				var numeros = SepararNumeroEmDigitos(numero);
				var novoNumero = numeros.Sum(n => n * n);
				var ehFeliz = PosicaoDaFelicidade(novoNumero, iteracoes - 1);
				retorno = ehFeliz + (ehFeliz >= 0 ? 1 : 0);
			}

			return retorno;
		}

		public IEnumerable<Int64> SepararNumeroEmDigitos(Int64 numero)
		{
			return ObterDigitos(numero).Reverse();
		}

		private IEnumerable<Int64> ObterDigitos(Int64 numero)
		{
			if (numero == 0L)
				yield return numero;
			else
			{
				while (numero > 0L)
				{
					yield return (numero % 10L);
					numero = numero / 10L;
				}
			}
		}
	}
}