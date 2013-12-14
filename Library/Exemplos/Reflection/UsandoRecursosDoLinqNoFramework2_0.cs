namespace MPSC.Library.Exemplos.Reflection
{
	using System;

	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class Extension : Attribute { }

	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method)]
	public sealed class ExtensionAttribute : Attribute { }
}

namespace System.Runtime.CompilerServices
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class ExtensionAttribute : Attribute { }
}

namespace System.Linq
{
	using System;
	using System.Collections.Generic;

	public delegate TResult Func<TP1, TResult>(TP1 p1);

	public static class LinqExtension
	{
		public static Boolean Any<T>(this IList<T> lista)
		{
			return lista.Count > 0;
		}

		public static IList<T> OrderBy<T>(this IList<T> lista, Func<T, T> acao) where T : IComparable, IComparable<T>
		{
			Comparison<T> comparison = (a, b) => acao(a).CompareTo(acao(b));
			var vLista = new List<T>();
			vLista.AddRange(lista);
			vLista.Sort(comparison);
			return vLista;
		}

		public static T FirstOrDefault<T>(this IList<T> lista, Func<T, Boolean> acao)
		{
			T vRetorno = default(T);
			foreach (T item in lista)
			{
				if (acao(item))
					vRetorno = item;
			}
			return vRetorno;
		}

		public static IList<R> Select<T, R>(this IList<T> lista, Func<T, R> acao)
		{
			var vRetorno = new List<R>();
			foreach (T item in lista)
			{
				vRetorno.Add(acao(item));
			}
			return vRetorno;
		}

		public static IList<T> Distinct<T>(this IList<T> lista)
		{
			var vRetorno = new List<T>();
			foreach (T item in lista)
			{
				if (!vRetorno.Contains(item))
					vRetorno.Add(item);
			}
			return vRetorno;
		}

		public static List<T> ToList<T>(this IList<T> lista)
		{
			var vLista = new List<T>();
			vLista.AddRange(lista);
			return vLista;
		}


	}
}
