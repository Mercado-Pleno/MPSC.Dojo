namespace MPSC.Library.Exemplos.DesignPattern.Strategy.Classes
{
	using MPSC.Library.Exemplos.DesignPattern.Strategy.Comportamento;

	public class MiniSimuladorDePatos : IExecutavel
	{

		public void Executar()
		{
			Pato pato = new PatoSelvagem();
			pato.Mostar();
			pato.FazerBarulho();
			pato.Voar();

			pato = new PatoDeBorracha();
			pato.Mostar();
			pato.FazerBarulho();
			pato.Voar();

			pato.SetVoador(new VoaComoUmFoguete());
			pato.Mostar();
			pato.FazerBarulho();
			pato.Voar();

		}
	}
}