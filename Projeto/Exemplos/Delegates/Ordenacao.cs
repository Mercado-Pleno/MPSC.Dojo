using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.Delegates
{
	public delegate Boolean Comparar<T>(T a, T b);

	public static class Ordenacao
	{
		public static IEnumerable<Tipo> Ordenar<Tipo>(this IEnumerable<Tipo> colecao, Comparar<Tipo> comparar)
		{
			var lista = colecao.ToArray();

			for (var i = 0; i < lista.Length - 1; i++)
			{
				for (var j = i + 1; j < lista.Length; j++)
				{
					if (comparar(lista[i], lista[j]))
					{
						Tipo tmp = lista[i];
						lista[i] = lista[j];
						lista[j] = tmp;
					}
				}
			}

			return lista;
		}
	}
}