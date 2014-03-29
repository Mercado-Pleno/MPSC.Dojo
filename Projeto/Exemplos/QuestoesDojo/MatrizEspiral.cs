namespace MPSC.Library.Exemplos.QuestoesDojo
{
	using MPSC.Library.Aula.Curso.DojoOnLine;
	public class MatrizEspiral : IExecutavel
	{
		public void Executar()
		{
			Espiral espiral = new Espiral();
			var matriz = espiral.GerarMatrizEspiral(24, 19);
			espiral.Print(matriz);
		}
	}
}