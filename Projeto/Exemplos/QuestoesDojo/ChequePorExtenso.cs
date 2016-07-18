using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo
{
	public class ChequePorExtensoRunner : IExecutavel
	{
		public void Executar()
		{
			Console.Write("Informe um número para representação em Algarísmos Romanos: ");
			var num = Console.ReadLine();
			var numero = num.Trim().All(c => Char.IsDigit(c) || c == ',') ? Convert.ToDecimal("0" + num.Trim()) : 0.0M;
			var numeroRomano = ChequePorExtenso.Novo(numero);
			Console.WriteLine(numeroRomano.ToString());
		}
	}


	public class ChequePorExtenso
	{
		public readonly Decimal Valor;
		public readonly String Descricao;

		public ChequePorExtenso(Decimal valor, String descricao)
		{
			Valor = valor;
			Descricao = descricao;
		}

		private enum Tipo { Unidade = 0, Dezena = 1, Centena = 2 }


		public static ChequePorExtenso Novo(Decimal numero)
		{
			var tipo = Tipo.Unidade;
			var descricao = "";
			var inteiro = Convert.ToInt64(numero);
			while (inteiro > 0)
			{
				long modulo = 0;
				if (tipo == Tipo.Unidade)
				{
					modulo = inteiro % 100;
					if (Entre(modulo, 01, 19))
					{
						descricao = cheques.First(c => c.Valor == modulo).Descricao + descricao;
						tipo = (Tipo)(((short)tipo + 1) % 3);
					}
					else
					{
						modulo = inteiro % 10;
						descricao = cheques.First(c => c.Valor == modulo).Descricao + descricao;
					}
				}

				else if (tipo == Tipo.Dezena)
				{
					modulo = inteiro % 100;
					if (Entre(modulo, 20, 99))
						descricao = cheques.First(c => c.Valor == modulo).Descricao + " e " + descricao;
				}

				else if (tipo == Tipo.Centena)
				{
					modulo = inteiro % 1000;
					if (Entre(modulo, 100, 999))
						descricao = cheques.First(c => c.Valor == modulo).Descricao + " e " + descricao;
				}

				inteiro -= modulo;
				tipo = (Tipo)(((short)tipo + 1) % 3);
			}

			return new ChequePorExtenso(numero, descricao);
		}

		private static Boolean Entre(Int64 valor, Int64 menor, Int64 maior)
		{
			return (valor >= menor) && (valor <= maior);
		}


		private static readonly List<ChequePorExtenso> cheques = new List<ChequePorExtenso>
		{
			N(0, ""), N(1, "um"), N(2, "dois"), N(3, "tres"), N(4, "quatro"), N(5, "cinco"), N(6, "seis"), N(7, "sete"), N(8, "oito"), N(9, "nove"),
			N(11, "onze"), N(12, "doze"), N(13, "treze"), N(14, "quatorze"), N(15, "quinze"), N(16, "dezesseis"), N(17, "dezesete"), N(18, "dezoito"), N(19, "dezenove"),
			N(10, "dez"), N(20, "vinte"), N(30, "trinta"), N(40, "quarenta"), N(50, "cinquenta"), N(60, "sessenta"), N(70, "setenta"), N(80, "oitenta"), N(90, "noventa"),
			N(100, "cem"), N(101, "cento"), N(200, "duzentos"), N(300, "trezentos"), N(400, "quatrocentos"), N(500, "quinhentos"), N(600, "seiscentos"), N(700, "setecentos"), N(800, "oitocentos"), N(900, "novecentos"),
			N(1000, "mil"),
			N(1000000L, "milhão"), N(2000000L, "milhões"),
			N(1000000000L, "bilhão"), N(2000000000L, "bilhões"),
			N(1000000000000L, "trilhão"), N(2000000000000L, "trilhões")
		};

		private static ChequePorExtenso N(Int64 valor, String descricao)
		{
			return new ChequePorExtenso(valor, descricao);
		}
	}
}