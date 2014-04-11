namespace MPSC.Library.Exemplos.Utilidades
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;

	public class AlgumaCoisaComRelatoriosUsandoLinq : IExecutavel
	{
		public void Executar()
		{
			var relatorio = new Relatorio();
			Console.WriteLine(relatorio.DataSource.Any());
		}

		public static ListaDados PreencherDataSource()
		{
			ListaDados retorno = new ListaDados();
			for (int linha = 1; linha <= 10; linha++)
			{
				for (int coluna = 1; coluna <= 10; coluna++)
				{
					retorno.Add(new Dados() { Data = DateTime.Now.AddDays(linha * coluna), Produto = "A" + (linha * coluna).ToString(), Valor = (linha + 2) * (coluna + linha) });
				}
			}
			return retorno;
		}
	}

	public class Relatorio
	{
		public IList<String> Produtos { get { return AlgumaCoisaComRelatoriosUsandoLinq.PreencherDataSource().Select(dados => dados.Produto).Distinct().ToList(); } }
		public IList<String> Meses { get { return AlgumaCoisaComRelatoriosUsandoLinq.PreencherDataSource().Select(dados => dados.Mes).Distinct().ToList(); } }
		public ListaDados DataSource { get { return AlgumaCoisaComRelatoriosUsandoLinq.PreencherDataSource(); } }
	}

	public class ListaDados : List<Dados> { }

	public class Dados
	{
		public string Mes { get { return Data.ToString("MMMM", new CultureInfo("pt-BR")); } }
		public DateTime Data { get; set; }
		public String Produto { get; set; }
		public Double Valor { get; set; }
	}
}