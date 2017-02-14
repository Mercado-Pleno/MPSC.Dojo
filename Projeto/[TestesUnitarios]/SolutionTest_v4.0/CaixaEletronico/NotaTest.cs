using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico.Notas;
using System;
using System.Linq;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass()]
    public class NotaTest
    {
        [TestMethod()]
        public void Se_Clonar_10_Nota2_Deve_Retornar_10_Notas_de_2_Reais()
        {
            var nota = new Nota002();
            var quantidade = 10;
            var notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 2)); //Expressao Lambda
        }

        [TestMethod()]
        public void Se_Clonar_10_Nota5_Deve_Retornar_10_Notas_de_5_Reais()
        {
			var nota = new Nota005();
			var quantidade = 10;
            var notas = nota.Clonar(quantidade);

            Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 5));
        }

        [TestMethod()]
        public void Se_Clonar_10_Nota10_Deve_Retornar_10_Notas_de_10_Reais()
        {
			var nota = new Nota010();
			var quantidade = 10;
            var notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 10));
        }

        [TestMethod()]
        public void Se_Clonar_10_Nota20_Deve_Retornar_10_Notas_de_20_Reais()
        {
			var nota = new Nota020();
			var quantidade = 10;
            var notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 20));
        }

        [TestMethod()]
        public void Se_Clonar_10_Nota50_Deve_Retornar_10_Notas_de_50_Reais()
        {
			var nota = new Nota050();
			var quantidade = 10;
            var notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 50));
        }

        [TestMethod()]
        public void Se_Clonar_10_Nota100_Deve_Retornar_10_Notas_de_100_Reais()
        {
			var nota = new Nota100();
			var quantidade = 10;
            var notas = nota.Clonar(quantidade);

			Assert.AreEqual(quantidade, notas.Count());
            Assert.AreEqual(quantidade, notas.Count(n => n.Valor == 100));
        }

        [TestMethod()]
        public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_10_Reais_a_Comparacao_Deve_Retornar_1_Positivo()
        {
            var nota = new Nota020();
            var obj = new Nota010();
			var expected = 1;
			var notas = nota.CompareTo(obj);
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_20_Reais_a_Comparacao_Deve_Retornar_Zero()
        {
			var nota = new Nota020();
			var obj = new Nota020();
			var expected = 0;
			var notas = nota.CompareTo(obj);
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Nulo_a_Comparacao_Deve_Retornar_Zero()
        {
			var nota = new Nota020();
			Object obj = null;
			var expected = 0;
			var notas = nota.CompareTo(obj);
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Algum_Objeto_Que_Nao_Herda_De_Nota_a_Comparacao_Deve_Retornar_Zero()
        {
			var nota = new Nota020();
			var obj = new Object();
			var expected = 0;
			var notas = nota.CompareTo(obj);
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Se_Comparar_Uma_Nota_de_20_Reais_Com_Outra_Nota_De_50_Reais_a_Comparacao_Deve_Retornar_1_Negativo()
        {
            Nota nota = new Nota020();
            object obj = new Nota050();
            int expected = -1;
            int notas = nota.CompareTo(obj);
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Quando_Converter_Uma_Nota_de_10_Reais_Em_String_Deve_Retornar_RS_10_00()
        {
            Nota nota = new Nota010();
            string expected = "R$ 10,00";
            string notas = nota.ToString();
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Quando_Converter_Uma_Nota_de_20_Reais_Em_String_Deve_Retornar_RS_20_00()
        {
            Nota nota = new Nota020();
            string expected = "R$ 20,00";
            string notas = nota.ToString();
            Assert.AreEqual(expected, notas);
        }

        [TestMethod()]
        public void Quando_Converter_Uma_Nota_de_50_Reais_Em_String_Deve_Retornar_RS_50_00()
        {
            Nota nota = new Nota050();
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