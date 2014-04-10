using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MP.Library.TestesUnitarios.SolutionTest
{
	[TestClass]
	public class PivotTest
	{
		private Pivot<InformacaoVendaDTO> GetPivotModelo1()
		{
			return new Pivot<InformacaoVendaDTO>()
				.AdicionarColunaFixa(d => d.NomeDoProduto)
				.AdicionarGrupo(d => d.Regiao)
				.Somar(d => d.QuantidadeVendida);
		}

		private Pivot<InformacaoVendaDTO> GetPivotModelo2()
		{
			return new Pivot<InformacaoVendaDTO>()
				.AdicionarColunaFixa(d => d.NomeDoProduto)
				.AdicionarGrupo(d => d.DataDaVenda.ToString("dd/MM/yyyy"))
				.Somar(d => d.QuantidadeVendida);
		}

		[TestMethod]
		public void Deve_Ter_Duas_Linhas_e_Duas_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource1()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource1());

			Assert.AreEqual(2, vDados.TotalDeLinhas());
			Assert.AreEqual(2, vDados.TotalDeColunas());
		}

		[TestMethod]
		public void Deve_Ter_Duas_Linhas_e_Tres_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource2()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource2());

			Assert.AreEqual(2, vDados.TotalDeLinhas());
			Assert.AreEqual(3, vDados.TotalDeColunas());
		}

		[TestMethod]
		public void Deve_Ter_Duas_Linhas_e_Quatro_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource3()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource3());

			Assert.AreEqual(2, vDados.TotalDeLinhas());
			Assert.AreEqual(4, vDados.TotalDeColunas());
		}

		[TestMethod]
		public void Deve_Ter_Tres_Linhas_e_Cinco_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource4()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource4());

			Assert.AreEqual(3, vDados.TotalDeLinhas());
			Assert.AreEqual(5, vDados.TotalDeColunas());
		}


		[TestMethod]
		public void Deve_Ter_Tres_Linhas_e_Cinco_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource5()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource5());

			Assert.AreEqual(3, vDados.TotalDeLinhas());
			Assert.AreEqual(5, vDados.TotalDeColunas());
		}

		[TestMethod]
		public void Deve_Ter_Tres_Linhas_e_Cinco_Colunas_Se_Usar_O_Pivot_Modelo1_Com_DataSource6()
		{
			var vPivot = GetPivotModelo1();
			var vDados = vPivot.TransformarDataSource(Dados.GetDataSource6());

			Assert.AreEqual(3, vDados.TotalDeLinhas());
			Assert.AreEqual(5, vDados.TotalDeColunas());
		}


		[TestMethod]
		public void Deve_Usar_O_Pivot_Modelo2_Com_DataSource6()
		{
			var dadosOriginais = Dados.GetDataSource6().ToList();
			var vPivot = GetPivotModelo2();
			var vDados = vPivot.TransformarDataSource(dadosOriginais);
			Assert.IsNotNull(vDados);
		}
	}


	public static class ArrayUtilExtension
	{
		public static int TotalDeLinhas(this Object[,] array)
		{
			return array.GetUpperBound(0) - array.GetLowerBound(0) + 1;
		}

		public static int TotalDeColunas(this Object[,] array)
		{
			return array.GetUpperBound(1) - array.GetLowerBound(1) + 1;
		}
	}


	public static class Dados
	{
		public static IEnumerable<InformacaoVendaDTO> GetDataSource1()
		{
			yield return new InformacaoVendaDTO("Monitor", 1, DateTime.Today.AddDays(-20), "RJ");
		}

		public static IEnumerable<InformacaoVendaDTO> GetDataSource2()
		{
			foreach (var item in GetDataSource1())
				yield return item;
			yield return new InformacaoVendaDTO("Monitor", 2, DateTime.Today.AddDays(-15), "SP");
		}

		public static IEnumerable<InformacaoVendaDTO> GetDataSource3()
		{
			foreach (var item in GetDataSource2())
				yield return item;
			yield return new InformacaoVendaDTO("Monitor", 3, DateTime.Today.AddDays(-10), "MG");
		}

		public static IEnumerable<InformacaoVendaDTO> GetDataSource4()
		{
			foreach (var item in GetDataSource3())
				yield return item;
			yield return new InformacaoVendaDTO("Mouse", 4, DateTime.Today.AddDays(-5), "BA");
		}

		public static IEnumerable<InformacaoVendaDTO> GetDataSource5()
		{
			foreach (var item in GetDataSource4())
				yield return item;
			yield return new InformacaoVendaDTO("Monitor", 5, DateTime.Today, "BA");
		}

		public static IEnumerable<InformacaoVendaDTO> GetDataSource6()
		{
			foreach (var item in GetDataSource5())
				yield return item;
			yield return new InformacaoVendaDTO("Monitor", 6, DateTime.Today.AddDays(+5), "BA");
		}

	}

	public class InformacaoVendaDTO
	{
		public String NomeDoProduto { get; set; }
		public int QuantidadeVendida { get; set; }
		public DateTime DataDaVenda { get; set; }
		public String Regiao { get; set; }

		public InformacaoVendaDTO(String produto, int quantidadeVendida, DateTime data, String regiao)
		{
			NomeDoProduto = produto;
			QuantidadeVendida = quantidadeVendida;
			DataDaVenda = data;
			Regiao = regiao;
		}
	}
}