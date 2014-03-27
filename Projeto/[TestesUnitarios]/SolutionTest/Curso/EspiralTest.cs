using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Aula.Curso.DojoOnLine;

namespace MPSC.Library.TestesUnitarios.SolutionTest.DojoOnLine
{
    [TestClass]
    public class EspiralTest
    {
        [TestMethod]
        public void DeveMontarMatrizEspiral50Por5()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(50, 5);
            Assert.AreEqual(250, matriz.Cast<int>().Count(i => i != 0));
            Assert.AreEqual(250, matriz.Cast<int>().Max(i => i));
            Assert.AreEqual(250, matriz.Cast<int>().Distinct().Count());
        }

        public void DeveMontarMatrizEspiral2Por99()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(2, 99);
            Assert.AreEqual(198, matriz.Cast<int>().Count(i => i != 0));
            Assert.AreEqual(198, matriz.Cast<int>().Max(i => i));
            Assert.AreEqual(198, matriz.Cast<int>().Distinct().Count());
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral1Por1()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(1, 1);
            Assert.AreEqual(1, matriz[0, 0]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral1Por2()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(1, 2);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[0, 1]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral2Por1()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(2, 1);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[1, 0]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral2Por2()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(2, 2);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[0, 1]);
            Assert.AreEqual(4, matriz[1, 0]);
            Assert.AreEqual(3, matriz[1, 1]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral2Por3()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(2, 3);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[0, 1]);
            Assert.AreEqual(3, matriz[0, 2]);
            Assert.AreEqual(6, matriz[1, 0]);
            Assert.AreEqual(5, matriz[1, 1]);
            Assert.AreEqual(4, matriz[1, 2]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral3Por2()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(3, 2);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[0, 1]);
            Assert.AreEqual(6, matriz[1, 0]);
            Assert.AreEqual(3, matriz[1, 1]);
            Assert.AreEqual(5, matriz[2, 0]);
            Assert.AreEqual(4, matriz[2, 1]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral3Por3()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(3, 3);
            Assert.AreEqual(1, matriz[0, 0]);
            Assert.AreEqual(2, matriz[0, 1]);
            Assert.AreEqual(3, matriz[0, 2]);
            Assert.AreEqual(8, matriz[1, 0]);
            Assert.AreEqual(9, matriz[1, 1]);
            Assert.AreEqual(4, matriz[1, 2]);
            Assert.AreEqual(7, matriz[2, 0]);
            Assert.AreEqual(6, matriz[2, 1]);
            Assert.AreEqual(5, matriz[2, 2]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral3Por4()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(3, 4);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(04, matriz[0, 3]);
            Assert.AreEqual(10, matriz[1, 0]);
            Assert.AreEqual(11, matriz[1, 1]);
            Assert.AreEqual(12, matriz[1, 2]);
            Assert.AreEqual(05, matriz[1, 3]);
            Assert.AreEqual(09, matriz[2, 0]);
            Assert.AreEqual(08, matriz[2, 1]);
            Assert.AreEqual(07, matriz[2, 2]);
            Assert.AreEqual(06, matriz[2, 3]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral4Por3()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(4, 3);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(10, matriz[1, 0]);
            Assert.AreEqual(11, matriz[1, 1]);
            Assert.AreEqual(04, matriz[1, 2]);
            Assert.AreEqual(09, matriz[2, 0]);
            Assert.AreEqual(12, matriz[2, 1]);
            Assert.AreEqual(05, matriz[2, 2]);
            Assert.AreEqual(08, matriz[3, 0]);
            Assert.AreEqual(07, matriz[3, 1]);
            Assert.AreEqual(06, matriz[3, 2]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral4Por4()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(4, 4);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(04, matriz[0, 3]);
            Assert.AreEqual(12, matriz[1, 0]);
            Assert.AreEqual(13, matriz[1, 1]);
            Assert.AreEqual(14, matriz[1, 2]);
            Assert.AreEqual(05, matriz[1, 3]);
            Assert.AreEqual(11, matriz[2, 0]);
            Assert.AreEqual(16, matriz[2, 1]);
            Assert.AreEqual(15, matriz[2, 2]);
            Assert.AreEqual(06, matriz[2, 3]);
            Assert.AreEqual(10, matriz[3, 0]);
            Assert.AreEqual(09, matriz[3, 1]);
            Assert.AreEqual(08, matriz[3, 2]);
            Assert.AreEqual(07, matriz[3, 3]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral5Por5()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(5, 5);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(04, matriz[0, 3]);
            Assert.AreEqual(05, matriz[0, 4]);
            Assert.AreEqual(16, matriz[1, 0]);
            Assert.AreEqual(17, matriz[1, 1]);
            Assert.AreEqual(18, matriz[1, 2]);
            Assert.AreEqual(19, matriz[1, 3]);
            Assert.AreEqual(06, matriz[1, 4]);
            Assert.AreEqual(15, matriz[2, 0]);
            Assert.AreEqual(24, matriz[2, 1]);
            Assert.AreEqual(25, matriz[2, 2]);
            Assert.AreEqual(20, matriz[2, 3]);
            Assert.AreEqual(07, matriz[2, 4]);
            Assert.AreEqual(14, matriz[3, 0]);
            Assert.AreEqual(23, matriz[3, 1]);
            Assert.AreEqual(22, matriz[3, 2]);
            Assert.AreEqual(21, matriz[3, 3]);
            Assert.AreEqual(08, matriz[3, 4]);
            Assert.AreEqual(13, matriz[4, 0]);
            Assert.AreEqual(12, matriz[4, 1]);
            Assert.AreEqual(11, matriz[4, 2]);
            Assert.AreEqual(10, matriz[4, 3]);
            Assert.AreEqual(09, matriz[4, 4]);
        }

        [TestMethod]
        public void DeveMontarMatrizEspiral5Por6()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(5, 6);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(04, matriz[0, 3]);
            Assert.AreEqual(05, matriz[0, 4]);
            Assert.AreEqual(06, matriz[0, 5]);

            Assert.AreEqual(18, matriz[1, 0]);
            Assert.AreEqual(19, matriz[1, 1]);
            Assert.AreEqual(20, matriz[1, 2]);
            Assert.AreEqual(21, matriz[1, 3]);
            Assert.AreEqual(22, matriz[1, 4]);
            Assert.AreEqual(07, matriz[1, 5]);

            Assert.AreEqual(17, matriz[2, 0]);
            Assert.AreEqual(28, matriz[2, 1]);
            Assert.AreEqual(29, matriz[2, 2]);
            Assert.AreEqual(30, matriz[2, 3]);
            Assert.AreEqual(23, matriz[2, 4]);
            Assert.AreEqual(08, matriz[2, 5]);

            Assert.AreEqual(16, matriz[3, 0]);
            Assert.AreEqual(27, matriz[3, 1]);
            Assert.AreEqual(26, matriz[3, 2]);
            Assert.AreEqual(25, matriz[3, 3]);
            Assert.AreEqual(24, matriz[3, 4]);
            Assert.AreEqual(09, matriz[3, 5]);

            Assert.AreEqual(15, matriz[4, 0]);
            Assert.AreEqual(14, matriz[4, 1]);
            Assert.AreEqual(13, matriz[4, 2]);
            Assert.AreEqual(12, matriz[4, 3]);
            Assert.AreEqual(11, matriz[4, 4]);
            Assert.AreEqual(10, matriz[4, 5]);
        }


        [TestMethod]
        public void DeveMontarMatrizEspiral6Por5()
        {
            var espiral = new Espiral();
            var matriz = espiral.GerarMatrizEspiral(6, 5);
            Assert.AreEqual(01, matriz[0, 0]);
            Assert.AreEqual(02, matriz[0, 1]);
            Assert.AreEqual(03, matriz[0, 2]);
            Assert.AreEqual(04, matriz[0, 3]);
            Assert.AreEqual(05, matriz[0, 4]);

            Assert.AreEqual(18, matriz[1, 0]);
            Assert.AreEqual(19, matriz[1, 1]);
            Assert.AreEqual(20, matriz[1, 2]);
            Assert.AreEqual(21, matriz[1, 3]);
            Assert.AreEqual(06, matriz[1, 4]);

            Assert.AreEqual(17, matriz[2, 0]);
            Assert.AreEqual(28, matriz[2, 1]);
            Assert.AreEqual(29, matriz[2, 2]);
            Assert.AreEqual(22, matriz[2, 3]);
            Assert.AreEqual(07, matriz[2, 4]);

            Assert.AreEqual(16, matriz[3, 0]);
            Assert.AreEqual(27, matriz[3, 1]);
            Assert.AreEqual(30, matriz[3, 2]);
            Assert.AreEqual(23, matriz[3, 3]);
            Assert.AreEqual(08, matriz[3, 4]);

            Assert.AreEqual(15, matriz[4, 0]);
            Assert.AreEqual(26, matriz[4, 1]);
            Assert.AreEqual(25, matriz[4, 2]);
            Assert.AreEqual(24, matriz[4, 3]);
            Assert.AreEqual(09, matriz[4, 4]);

            Assert.AreEqual(14, matriz[5, 0]);
            Assert.AreEqual(13, matriz[5, 1]);
            Assert.AreEqual(12, matriz[5, 2]);
            Assert.AreEqual(11, matriz[5, 3]);
            Assert.AreEqual(10, matriz[5, 4]);

            espiral.Print(matriz);
        }
    }
}
