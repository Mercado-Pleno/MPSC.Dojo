using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class NumeroSortudo
	{
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