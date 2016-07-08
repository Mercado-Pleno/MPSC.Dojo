using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.Utilidades.Extensions
{
	public class ArvoreUtopica : IExecutavel
	{
		static int[] mem = Enumerable.Range(0, 61).Select(x => -1).ToArray();
		public void Executar()
		{
			Inputs.Init();
			var intEnum = Inputs.GetIntStream();
			int quantidade = intEnum.First();

			intEnum.Take(quantidade).ForAll(x =>
			{
				Console.WriteLine(getHeight(x));
			});
		}

		private static int getHeight(int x)
		{
			if (mem[x] != -1) return mem[x];
			if (x == 0) return 1;
			return mem[x] = (x % 2 == 1) ? (getHeight(x - 1) * 2) : (getHeight(x - 1) + 1);
		}
	}

	static class Inputs
	{
		public static IEnumerator<string> MyStream { get; private set; }

		public static void Init(char separator = ' ')
		{
			MyStream = Stream(separator).GetEnumerator();
		}

		static IEnumerable<string> Stream(char separator = ' ')
		{
			while (true)
			{
				var streamarr = SingleLineStream(separator);
				foreach (string item in streamarr)
					yield return item;
			}
		}
		public static IEnumerable<string> SingleLineStream(char separator = ' ')
		{
			return Console.ReadLine().Split(separator);
		}

		public static IEnumerable<int> GetIntStream()
		{
			return ValuesStream(x => int.Parse(x));
		}

		public static IEnumerable<T> ValuesStream<T>(Func<string, T> func)
		{
			while (true)
				yield return func(MyStream.Next());
		}

		public static int[] IntArray(char separator = ' ')
		{
			return SingleLineStream(separator).Select(x => int.Parse(x)).ToArray();
		}

		public static int Int()
		{
			return int.Parse(MyStream.Next());
		}
	}

	public static class Extensions
	{
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a)
		{
			a = enumer.Next();
			return enumer;
		}
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a, out T b)
		{
			return enumer.Assign(out a).Assign(out b); ;
		}
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a, out T b, out T c)
		{
			return enumer.Assign(out a, out b).Assign(out c);
		}
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a, out T b, out T c, out T d)
		{
			return enumer.Assign(out a, out b).Assign(out c, out d);
		}
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a, out T b, out T c, out T d, out T e)
		{
			return enumer.Assign(out a, out b).Assign(out c, out d, out e);
		}
		public static IEnumerator<T> Assign<T>(this IEnumerator<T> enumer, out T a, out T b, out T c, out T d, out T e, out T f)
		{
			return enumer.Assign(out a, out b, out c).Assign(out d, out e, out f);
		}
		public static T Next<T>(this IEnumerator<T> enumer)
		{
			enumer.MoveNext();
			return enumer.Current;
		}
		public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
		{
			foreach (T item in sequence)
				action(item);
		}
	}
}