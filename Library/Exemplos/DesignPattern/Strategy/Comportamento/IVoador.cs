namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento
{
	using System;

	public interface IVoador
	{
		void Voar();
	}

	public class Voa : IVoador
	{
		public void Voar()
		{
			Console.WriteLine("Eu estou voando!");
		}
	}

	public class NaoVoa : IVoador
	{
		public void Voar()
		{
			Console.WriteLine("< < Eu não consigo voar :-( > >");
		}
	}

	public class VoaComoUmFoguete : IVoador
	{
		public void Voar()
		{
			Console.WriteLine("Eu estou voando como um foguete!");
		}
	}
}