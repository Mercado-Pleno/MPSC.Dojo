using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
	public static class Program
	{
		public static void Executar()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new Form1(new Relatorio()));
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
		public IList<String> Produtos { get { return Program.PreencherDataSource().Select(dados => dados.Produto).Distinct().ToList(); } }
		public IList<String> Meses { get { return Program.PreencherDataSource().Select(dados => dados.Mes).Distinct().ToList(); } }
		public ListaDados DataSource { get { return Program.PreencherDataSource(); } }
	}

	public class ListaDados : List<Dados>, IList<Dados>, IEnumerable<Dados> { }

	public class Dados
	{
		public string Mes { get { return Data.ToString("MMMM", new CultureInfo("pt-BR")); } }
		public DateTime Data { get; set; }
		public String Produto { get; set; }
		public Double Valor { get; set; }
	}
}