using CaixaEletronico;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
{
    [TestClass()]
    public class Nota10Test
    {
        [TestMethod]
        public void Se_a_Nota_For_Uma_Nota_de_10_Reais_o_Valor_da_Nota_Deve_Ser_10()
        {
            Nota nota = new Nota10();

            Assert.AreEqual(10, nota.Valor, "Valor da Nota");
        }
    }
}