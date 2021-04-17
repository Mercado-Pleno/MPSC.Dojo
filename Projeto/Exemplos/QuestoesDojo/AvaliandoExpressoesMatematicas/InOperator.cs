using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public static class InOperator
	{
		public static string Join<T>(this IEnumerable<T> lista, string join)
		{
			return string.Join(join, lista);
		}

		public static bool In<T>(this T self, params T[] lista)
		{
			return lista.Contains(self);
		}

		public static bool In<T>(this T self, IEnumerable<T> lista)
		{
			return lista.Contains(self);
		}


		public static int IndexOfAny<T>(this IEnumerable<T> self, bool tokenPriority, IEnumerable<T> tokens)
		{
			var retorno = -1;
			foreach (var token in tokens)
			{
				var index = self.IndexOf(token);
				if (index >= 0)
				{
					if (tokenPriority)
						return index;
					else if (index < retorno)
						retorno = index;
				}
			}
			return retorno;
		}

		public static int IndexOf<T>(this IEnumerable<T> lista, T token)
		{
			var i = 0;
			foreach (var item in lista)
			{
				if (item.Equals(token))
					return i;
				i++;
			}
			return -1;
		}
	}
}