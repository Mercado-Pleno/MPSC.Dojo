using System;
using System.Collections.Generic;

namespace MPSC.Library.Exemplos.Delegates
{
	public class ProcessandoOrdenacaoComDelegate : IExecutavel
	{
		public void Executar()
		{
			var listaOriginal = Extension.ObterLista();
			listaOriginal.Print("listaOriginal");

			var listaOrdenadaPorNascimentoAsc = listaOriginal.Ordenar(delegate(Pessoa p1, Pessoa p2) { return p1.Nascimento < p2.Nascimento; });
			listaOrdenadaPorNascimentoAsc.Print("listaOrdenadaPorNascimentoAsc");

			var listaOrdenadaPorNascimentoDesc = listaOriginal.Ordenar((p1, p2) => p1.Nascimento > p2.Nascimento);
			listaOrdenadaPorNascimentoDesc.Print("listaOrdenadaPorNascimentoDesc");

			var listaOrdenadaPorVariosFatores = listaOriginal.Ordenar(CompararPorVariosFatores);
			listaOrdenadaPorVariosFatores.Print("listaOrdenadaPorVariosFatores");
		}

		public Boolean CompararPorVariosFatores(Pessoa p1, Pessoa p2)
		{
			if (p1.Nascimento != p2.Nascimento)
				return p1.Nascimento < p2.Nascimento;
			else if (p1.TempoExperiencia != p2.TempoExperiencia)
				return p1.TempoExperiencia < p2.TempoExperiencia;
			else
				return p1.Idade < p2.Idade;
		}
	}


	public class Pessoa
	{
		public String Nome { get; private set; }
		public DateTime Nascimento { get; private set; }
		public Int32 Idade { get { return Convert.ToInt32((DateTime.Today - Nascimento).TotalDays / 365); } }
		public Int32 TempoExperiencia { get; private set; }

		public Pessoa(string nome, DateTime nascimento, int tempoExperiencia)
		{
			Nome = nome;
			Nascimento = nascimento;
			TempoExperiencia = tempoExperiencia;
		}
		public override String ToString()
		{
			return String.Format("{0} {1} {2} {3}", Nome, Nascimento.ToString("MM/yyyy"), Idade.ToString().PadLeft(2, '0'), TempoExperiencia.ToString().PadLeft(2, '0'));
		}
	}

	public static class Extension
	{
		public static List<Pessoa> ObterLista()
		{
			return new List<Pessoa>
			{
				new Pessoa("01", new DateTime(1979, 01, 01), 11),
				new Pessoa("02", new DateTime(1979, 01, 01), 09),
				new Pessoa("03", new DateTime(1979, 01, 01), 10),
				new Pessoa("04", new DateTime(1980, 04, 01), 09),
				new Pessoa("05", new DateTime(1980, 04, 01), 10),
				new Pessoa("06", new DateTime(1980, 04, 01), 11),
				new Pessoa("07", new DateTime(1981, 07, 01), 10),
				new Pessoa("08", new DateTime(1981, 07, 01), 09),
				new Pessoa("09", new DateTime(1981, 07, 01), 11),
				new Pessoa("10", new DateTime(2016, 10, 01), 00),
			};
		}

		public static void Print(this IEnumerable<Pessoa> lista, String tipo)
		{
			Console.WriteLine(tipo);
			foreach (var item in lista)
				Console.WriteLine(item.ToString());
		}
	}
}