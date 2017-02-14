using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP.Library.CaixaEletronico.Notas;

namespace MPSC.Library.TestesUnitarios.SolutionTest
{
	[TestClass()]
    public class Nota100Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_100_Reais_o_Valor_da_Nota_Deve_Ser_100()
        {
            Nota nota = new Nota100();
            Assert.IsNotNull(nota, "Objeto da classe Nota");
            Assert.IsNotNull(nota.Valor, "Valor");

            Assert.AreEqual(100, nota.Valor, "Valor da Nota");
        }
    }
}