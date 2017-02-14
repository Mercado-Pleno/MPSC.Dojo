﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico.Notas;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass()]
    public class Nota5Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_5_Reais_o_Valor_da_Nota_Deve_Ser_5()
        {
            Nota nota = new Nota005();

            Assert.AreEqual(5, nota.Valor, "Valor da Nota");
        }
    }
}