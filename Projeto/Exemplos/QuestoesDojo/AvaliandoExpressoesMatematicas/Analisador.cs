using System;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public class Analisador
	{
		private decimal sinal = 1M;
		private decimal? ObterValor(string elemento)
		{
			if (elemento == "-")
				InverterSinal();
			else if (elemento != "+")
			{
				var numero = Convert.ToDecimal(elemento) * sinal;
				ResetSinal();
				return numero;
			}

			return null;
		}

		private void InverterSinal()
		{
			sinal *= -1M;
		}

		private void ResetSinal()
		{
			sinal = 1M;
		}
	}
}