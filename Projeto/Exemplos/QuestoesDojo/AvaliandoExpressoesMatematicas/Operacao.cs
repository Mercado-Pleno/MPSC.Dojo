using System;
using System.Globalization;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public abstract class Operacao
	{
		public static readonly CultureInfo pt_BR = new CultureInfo("pt-BR");
		private readonly Func<decimal, decimal, decimal> _calculo;
		public Operacao(Func<decimal, decimal, decimal> calculo) { _calculo = calculo; }

		protected virtual decimal ParseToDecimal(string numero)
		{
			return decimal.Parse(numero, NumberStyles.Number, pt_BR);
		}

		public virtual string Calcular(string numero1, string numero2)
		{
			return Calcular(ParseToDecimal(numero1), ParseToDecimal(numero2)).ToString(pt_BR);
		}

		public virtual decimal Calcular(decimal numero1, decimal numero2)
		{
			return _calculo.Invoke(numero1, numero2);
		}
	}
}