using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
    [TestClass()]
    public class Nota20Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_20_Reais_o_Valor_da_Nota_Deve_Ser_20()
        {
            Nota nota = new Nota20();

            Assert.AreEqual(20, nota.Valor, "Valor da Nota");
        }
    }
}