using System;
using System.Collections.Generic;
using System.Linq;
using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
	[TestClass()]
	public class NotaTest
	{
		[TestMethod()]
		public void Se_Clonar_10_Nota2_Deve_Retornar_10_Notas_de_2_Reais()
		{
			Nota nota = new Nota2();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade); 

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 2)); //Expressao Lambda
		}

		[TestMethod()]
		public void Se_Clonar_10_Nota5_Deve_Retornar_10_Notas_de_5_Reais()
		{
			Nota nota = new Nota5();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 5));
		}

		[TestMethod()]
		public void Se_Clonar_10_Nota10_Deve_Retornar_10_Notas_de_10_Reais()
		{
			Nota nota = new Nota10();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 10));
		}

		[TestMethod()]
		public void Se_Clonar_10_Nota20_Deve_Retornar_10_Notas_de_20_Reais()
		{
			Nota nota = new Nota20();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 20));
		}

		[TestMethod()]
		public void Se_Clonar_10_Nota50_Deve_Retornar_10_Notas_de_50_Reais()
		{
			Nota nota = new Nota50();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 50));
		}

		[TestMethod()]
		public void Se_Clonar_10_Nota100_Deve_Retornar_10_Notas_de_100_Reais()
		{
			Nota nota = new Nota100();
			int quantidade = 10;
			List<Nota> notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count);
			Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 100));
		}

		[TestMethod()]
		public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_10_Reais_a_Comparacao_Deve_Retornar_1_Positivo()
		{
			Nota nota = new Nota20();
			object obj = new Nota10();
			int expected = 1;
			int notas = nota.CompareTo(obj);
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_20_Reais_a_Comparacao_Deve_Retornar_Zero()
		{
			Nota nota = new Nota20();
			object obj = new Nota20();
			int expected = 0;
			int notas = nota.CompareTo(obj);
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Nulo_a_Comparacao_Deve_Retornar_Zero()
		{
			Nota nota = new Nota20();
			object obj = null;
			int expected = 0;
			int notas = nota.CompareTo(obj);
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Algum_Objeto_Que_Nao_Herda_De_Nota_a_Comparacao_Deve_Retornar_Zero()
		{
			Nota nota = new Nota20();
			object obj = new Object();
			int expected = 0;
			int notas = nota.CompareTo(obj);
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_50_Reais_a_Comparacao_Deve_Retornar_1_Negativo()
		{
			Nota nota = new Nota20();
			object obj = new Nota50();
			int expected = -1;
			int notas = nota.CompareTo(obj);
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Quando_Converter_Uma_Nota_de_10_Reais_Em_String_Deve_Retornar_RS_10_00()
		{
			Nota nota = new Nota10();
			string expected = "R$ 10,00";
			string notas = nota.ToString();
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Quando_Converter_Uma_Nota_de_20_Reais_Em_String_Deve_Retornar_RS_20_00()
		{
			Nota nota = new Nota20();
			string expected = "R$ 20,00";
			string notas = nota.ToString();
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Quando_Converter_Uma_Nota_de_50_Reais_Em_String_Deve_Retornar_RS_50_00()
		{
			Nota nota = new Nota50();
			string expected = "R$ 50,00";
			string notas = nota.ToString();
			Assert.AreEqual(expected, notas);
		}

		[TestMethod()]
		public void Quando_Converter_Uma_Nota_de_100_Reais_Em_String_Deve_Retornar_RS_100_00()
		{
			Nota nota = new Nota100();
			string expected = "R$ 100,00";
			string notas = nota.ToString();
			Assert.AreEqual(expected, notas);
		}
	}
}