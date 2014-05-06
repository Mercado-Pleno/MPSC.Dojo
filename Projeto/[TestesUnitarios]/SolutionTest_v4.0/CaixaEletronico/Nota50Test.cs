using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
    [TestClass()]
    public class Nota50Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_50_Reais_o_Valor_da_Nota_Deve_Ser_50()
        {
            Nota nota = new Nota50();

            Assert.AreEqual(50, nota.Valor, "Valor da Nota");
        }
    }
}