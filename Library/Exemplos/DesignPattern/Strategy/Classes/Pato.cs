namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Classes
{
	using System;
	using MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento;

	public abstract class Pato
	{
		protected IVoador voador { get; set; }
		protected IBarulho barulho { get; set; }

		public Pato()
		{
			voador = new Voa();
			barulho = new Grasnar();
		}

		public void Mostar()
		{
			Console.WriteLine();
			Console.WriteLine("Eu sou um Pato! Sou um " + this.GetType().Name);
		}

		public void SetVoador(IVoador voa)
		{
			this.voador = voa;
		}

		public void FazerBarulho()
		{
			barulho.FazerBarulho();
		}

		public void Voar()
		{
			voador.Voar();
		}
	}
}