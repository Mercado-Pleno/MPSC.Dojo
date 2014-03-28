﻿using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
    [TestClass()]
    public class Nota2Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_2_Reais_o_Valor_da_Nota_Deve_Ser_2()
        {
            Nota nota = new Nota2();

            Assert.AreEqual(2, nota.Valor, "Valor da Nota");
        }
    }
}