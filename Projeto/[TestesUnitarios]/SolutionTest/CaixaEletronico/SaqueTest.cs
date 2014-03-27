using System;
using System.Collections.Generic;
using System.Linq;
using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
    [TestClass]
    public class SaqueTest
    {
        [TestMethod]
        public void Se_Sacar_10_Reais_Deve_Retornar_1_Nota_de_10_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(10);

            Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
            Assert.AreEqual(10, notas.Sum(n => n.Valor), "Valor do Saque");
            Assert.IsInstanceOfType(notas[0], typeof(Nota10), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_20_Reais_Deve_Retornar_1_Nota_de_20_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(20);

            Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
            Assert.AreEqual(20, notas.Sum(n => n.Valor), "Valor do Saque");
            Assert.IsInstanceOfType(notas[0], typeof(Nota20), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_50_Reais_Deve_Retornar_1_Nota_de_50_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(50);

            Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
            Assert.AreEqual(50, notas.Sum(n => n.Valor), "Valor do Saque");
            Assert.IsInstanceOfType(notas[0], typeof(Nota50), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_100_Reais_Deve_Retornar_1_Nota_de_100_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(100);

            Assert.AreEqual(1, notas.Count, "Quantidade de Notas");
            Assert.AreEqual(100, notas.Sum(n => n.Valor), "Valor do Saque");
            Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Se_Tentar_Sacar_Centavos_ou_Valores_Menores_Que_10_Reais_Deve_Disparar_Excecao()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(7);

            Assert.Inconclusive("Nao Disparou Exception");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Se_Tentar_Sacar_Valores_Maiores_e_Nao_Multiplos_de_10_Reais_Deve_Disparar_Excecao()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(32);

            Assert.Inconclusive("Nao Disparou Exception");
        }

        [TestMethod]
        public void Se_Sacar_30_Reais_Deve_Retornar_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(30);

            Assert.AreEqual(2, notas.Count, "Quantidade de Notas");

            Assert.AreEqual(30, notas.Sum(n => n.Valor), "Valor do Saque");

            Assert.IsInstanceOfType(notas[0], typeof(Nota20), "Tipo da nota");
            Assert.IsInstanceOfType(notas[1], typeof(Nota10), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_80_Reais_Deve_Retornar_1_Nota_de_50_Reais_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(80);

            Assert.AreEqual(3, notas.Count, "Quantidade de Notas");

            Assert.AreEqual(80, notas.Sum(n => n.Valor), "Valor do Saque");

            Assert.IsInstanceOfType(notas[0], typeof(Nota50), "Tipo da nota");
            Assert.IsInstanceOfType(notas[1], typeof(Nota20), "Tipo da nota");
            Assert.IsInstanceOfType(notas[2], typeof(Nota10), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_180_Reais_Deve_Retornar_1_Nota_de_100_Reais_1_Nota_de_50_Reais_1_Nota_de_20_Reais_e_1_Nota_de_10_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(180);

            Assert.AreEqual(4, notas.Count, "Quantidade de Notas");

            Assert.AreEqual(180, notas.Sum(n => n.Valor), "Valor do Saque");

            Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
            Assert.IsInstanceOfType(notas[1], typeof(Nota50), "Tipo da nota");
            Assert.IsInstanceOfType(notas[2], typeof(Nota20), "Tipo da nota");
            Assert.IsInstanceOfType(notas[3], typeof(Nota10), "Tipo da nota");
        }

        [TestMethod]
        public void Se_Sacar_190_Reais_Deve_Retornar_1_Nota_de_100_Reais_1_Nota_de_50_Reais_2_Notas_de_20_Reais_e_0_Notas_de_10_Reais()
        {
            Saque saque = new Saque();

            IList<Nota> notas = saque.Sacar(190);

            Assert.AreEqual(4, notas.Count, "Quantidade de Notas");

            Assert.AreEqual(190, notas.Sum(n => n.Valor), "Valor do Saque");

            Assert.IsInstanceOfType(notas[0], typeof(Nota100), "Tipo da nota");
            Assert.IsInstanceOfType(notas[1], typeof(Nota50), "Tipo da nota");
            Assert.IsInstanceOfType(notas[2], typeof(Nota20), "Tipo da nota");
            Assert.IsInstanceOfType(notas[3], typeof(Nota20), "Tipo da nota");
        }
    }
}