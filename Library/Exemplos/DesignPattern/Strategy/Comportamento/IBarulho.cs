namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento
{
	using System;

	public interface IBarulho
	{
		void FazerBarulho();
	}

	public class Grasnar : IBarulho
	{
		public void FazerBarulho()
		{
			Console.WriteLine("Quack quack!");
		}
	}

	public class Squack : IBarulho
	{
		public void FazerBarulho()
		{
			Console.WriteLine("Squacke squacke!");
		}
	}

	public class Mudo : IBarulho
	{
		public void FazerBarulho()
		{
			Console.WriteLine("< < Silêncio > >");
		}
	}

	public class Miar : IBarulho
	{
		public void FazerBarulho()
		{
			Console.WriteLine("Miau!");
		}
	}

	public class Latir : IBarulho
	{
		public void FazerBarulho()
		{
			Console.WriteLine("Au Au!");
		}
	}
}