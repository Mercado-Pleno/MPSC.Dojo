using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class NumeroRomanoRunner : IExecutavel
	{
		public void Executar()
		{
			Console.Write("Informe um número para representação em Algarísmos Romanos: ");
			var num = Console.ReadLine();
			var numero = num.Trim().All(c => Char.IsDigit(c)) ? Convert.ToInt32("0" + num.Trim()) : 0;
			var numeroRomano = NumeroRomano.Novo(numero);
			Console.WriteLine(numeroRomano.ToString());
		}
	}

	public class NumeroRomano
	{
		public Int32 ValorSemantico { get; private set; }
		public String Representacao { get; private set; }

		private NumeroRomano(Int32 valorSemantico, String representacao)
		{
			ValorSemantico = valorSemantico;
			Representacao = representacao;
		}

		public override String ToString()
		{
			return String.Format("A representação do número {0} em Algarismos romanos é {1}", ValorSemantico, Representacao);
		}

		public static NumeroRomano Novo(Int32 numero)
		{
			var valorSemantico = 0;
			var representacao = (numero == 0) ? "O" : String.Empty;
			while (numero > 0)
			{
				var ultimoMenor = numerosRomanos.Last(nr => nr.ValorSemantico <= numero);
				valorSemantico += ultimoMenor.ValorSemantico;
				representacao += ultimoMenor.Representacao;
				numero -= ultimoMenor.ValorSemantico;
			}
			return new NumeroRomano(valorSemantico, representacao);
		}

		private static readonly List<NumeroRomano> numerosRomanos = new List<NumeroRomano> 
		{
			new NumeroRomano(0001, "I"), new NumeroRomano(0004, "IV"), new NumeroRomano(0005, "V"), new NumeroRomano(0009, "IX"),
			new NumeroRomano(0010, "X"), new NumeroRomano(0040, "XL"), new NumeroRomano(0050, "L"), new NumeroRomano(0090, "XC"),
			new NumeroRomano(0100, "C"), new NumeroRomano(0400, "CD"), new NumeroRomano(0500, "D"), new NumeroRomano(0900, "CM"),
			new NumeroRomano(1000, "M"), new NumeroRomano(4000, "iv"), new NumeroRomano(5000, "v"), new NumeroRomano(9000, "ix"),
		};
	}
}