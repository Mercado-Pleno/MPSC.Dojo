using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Job
{
	public class CalculadoraDeRaizQuadrada : IJobBase<Numero>
	{
		public Int32 ObterQuantidadeDeItensPorLote()
		{
			return 2;
		}

		public IEnumerable<Numero> ObterInformacoes()
		{
			return Numero.Gerar(10);
		}

		public Boolean ValidarLote(IEnumerable<Numero> lote)
		{
			return lote.All(n => n.Valor > 0);
		}

		public Boolean ValidarItem(Numero n)
		{
			return n.Valor > 0;
		}

		public void ProcessarItem(Numero n)
		{
			n.Resultado = Convert.ToDecimal(Math.Sqrt(n.Valor));
		}

		public void PosCondicaoItem(Numero n)
		{
			Console.WriteLine(n.ToString());
		}

		public void PosCondicaoLote(IEnumerable<Numero> lote)
		{
			lote.All(n => n.Valor > 0);
		}
	}

	public class Numero
	{
		public Int32 Valor { get; set; }
		public Decimal Resultado { get; set; }

		public override String ToString()
		{
			return String.Format("Sqrt({0}) = {1}", Valor, Resultado);
		}
		public static Numero[] Gerar(Int32 quantidade)
		{
			return Enumerable
				.Range(-quantidade / 2, quantidade / 2)
				.Select(n => new Numero { Valor = n })
				.ToArray();
		}
	}
}