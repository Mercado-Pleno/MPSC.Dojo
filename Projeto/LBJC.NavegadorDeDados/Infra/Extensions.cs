using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBJC.NavegadorDeDados
{
	public static class Extensions
	{
		public static String Concatenar<T>(this IEnumerable<T> source, String join)
		{
			return String.Join<T>(join, source);
		}
	}
}
