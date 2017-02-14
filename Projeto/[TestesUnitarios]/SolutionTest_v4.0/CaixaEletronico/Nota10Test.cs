using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico.Notas;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass()]
    public class Nota10Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_10_Reais_o_Valor_da_Nota_Deve_Ser_10()
        {
            Nota nota = new Nota010();

            Assert.AreEqual(10, nota.Valor, "Valor da Nota");
        }
    }
}