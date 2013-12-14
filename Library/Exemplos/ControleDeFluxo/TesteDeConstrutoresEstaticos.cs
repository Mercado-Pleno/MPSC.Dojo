namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	using System;

	public class TesteDeConstrutoresEstaticos : IExecutavel
	{
		public void Executar()
		{
			Console.WriteLine("X = {0}, Y = {1}", AA.X, BB.Y);
		}
	}

	public class AA
	{
		public static int X;
		static AA()
		{
			X = BB.Y + 1;
		}
	}

	public class BB
	{
		public static int Y = AA.X + 1;
		static BB()
		{

		}
	}
}