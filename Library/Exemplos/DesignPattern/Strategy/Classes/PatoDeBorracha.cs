namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Classes
{
	using MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento;

	public class PatoDeBorracha : Pato
	{
		public PatoDeBorracha()
		{
			voador = new NaoVoa();
			barulho = new Squack();
		}
	}
}