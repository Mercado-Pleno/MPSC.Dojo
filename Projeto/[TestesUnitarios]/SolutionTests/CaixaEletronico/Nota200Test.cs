using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
    [TestClass]
    public class Nota200Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_200_Reais_o_Valor_da_Nota_Deve_Ser_200()
        {
            Nota200 nota = new Nota200();

            Assert.AreEqual(200, nota.Valor, "Valor da Nota");
        }
    }
}