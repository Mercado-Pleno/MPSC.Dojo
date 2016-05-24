using System;
using System.Collections;
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

		public Boolean EhSortudo(Int64 numero, Int32 iteracoes)
		{
			var lista = GerarLista(1, 100);
			var sortudo = listaDeSortudos(lista, iteracoes);
			return sortudo.Contains(numero);
		}

		public List<Int64> listaDeSortudos(IEnumerable<Int64> numeros, Int32 iteracoes)
		{
			var lista = new List<Int64>(numeros);

			var posicao = 0;
			if (iteracoes-- > 0)
			{
				var modulo = lista[posicao + 1] % 2;

				foreach (var item in lista.ToArray())
				{
					if ((item % 2) == modulo)
						lista.Remove(item);
				}
			}

			while ((iteracoes-- > 0) && (++posicao < lista.Count))
			{
				var numero = lista[posicao];
				foreach (var item in lista.Skip(posicao).ToArray())
				{
					if ((item % numero) != 0)
						lista.Remove(item);
				}
			}

			return lista;
		}

		private IEnumerable<Int64> GerarLista(Int32 minimo, Int32 maximo)
		{
			return Enumerable.Range(minimo, maximo - minimo + 1).Select(n => (Int64)n);
		}
	}
}