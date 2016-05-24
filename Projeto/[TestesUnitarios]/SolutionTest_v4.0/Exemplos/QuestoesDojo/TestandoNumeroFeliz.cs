using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoNumeroFeliz
	{

		[TestMethod]
		public void QuantoPedePraSepararNumero0EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(0).ToArray();
			Assert.AreEqual(1, digitos.Length);
			Assert.AreEqual(0, digitos[0]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero01EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(1).ToArray();
			Assert.AreEqual(1, digitos.Length);
			Assert.AreEqual(1, digitos[0]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero05EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(5).ToArray();
			Assert.AreEqual(1, digitos.Length);
			Assert.AreEqual(5, digitos[0]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero12EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(12).ToArray();
			Assert.AreEqual(2, digitos.Length);
			Assert.AreEqual(1, digitos[0]);
			Assert.AreEqual(2, digitos[1]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero123EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(123).ToArray();
			Assert.AreEqual(3, digitos.Length);
			Assert.AreEqual(1, digitos[0]);
			Assert.AreEqual(2, digitos[1]);
			Assert.AreEqual(3, digitos[2]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero1234EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(1234).ToArray();
			Assert.AreEqual(4, digitos.Length);
			Assert.AreEqual(1, digitos[0]);
			Assert.AreEqual(2, digitos[1]);
			Assert.AreEqual(3, digitos[2]);
			Assert.AreEqual(4, digitos[3]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero12345EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(12345).ToArray();
			Assert.AreEqual(5, digitos.Length);
			Assert.AreEqual(1, digitos[0]);
			Assert.AreEqual(2, digitos[1]);
			Assert.AreEqual(3, digitos[2]);
			Assert.AreEqual(4, digitos[3]);
			Assert.AreEqual(5, digitos[4]);
		}

		[TestMethod]
		public void QuantoPedePraSepararNumero9223372036854775807EmDigitos()
		{
			var numeroFeliz = new NumeroFeliz();
			var digitos = numeroFeliz.SepararNumeroEmDigitos(9223372036854775807).ToArray();
			Assert.AreEqual(19, digitos.Length);
			Assert.AreEqual(9, digitos[00]);
			Assert.AreEqual(2, digitos[01]);
			Assert.AreEqual(2, digitos[02]);
			Assert.AreEqual(3, digitos[03]);
			Assert.AreEqual(3, digitos[04]);
			Assert.AreEqual(7, digitos[05]);
			Assert.AreEqual(2, digitos[06]);
			Assert.AreEqual(0, digitos[07]);
			Assert.AreEqual(3, digitos[08]);
			Assert.AreEqual(6, digitos[09]);
			Assert.AreEqual(8, digitos[10]);
			Assert.AreEqual(5, digitos[11]);
			Assert.AreEqual(4, digitos[12]);
			Assert.AreEqual(7, digitos[13]);
			Assert.AreEqual(7, digitos[14]);
			Assert.AreEqual(5, digitos[15]);
			Assert.AreEqual(8, digitos[16]);
			Assert.AreEqual(0, digitos[17]);
			Assert.AreEqual(7, digitos[18]);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero01_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(1, 100);
			Assert.AreEqual(0, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero10_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(10, 100);
			Assert.AreEqual(1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero130_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(130, 100);
			Assert.AreEqual(2, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero97_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(97, 100);
			Assert.AreEqual(3, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero49_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(49, 100);
			Assert.AreEqual(4, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero07_100()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(07, 100);
			Assert.AreEqual(5, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero01_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(1, 0);
			Assert.AreEqual(0, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero10_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(10, 0);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero130_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(130, 0);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero97_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(97, 0);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero49_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(49, 0);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero07_0()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(07, 0);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero01_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(1, 1);
			Assert.AreEqual(0, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero10_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(10, 1);
			Assert.AreEqual(1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero130_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(130, 1);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero97_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(97, 1);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero49_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(49, 1);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero07_1()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(07, 1);
			Assert.AreEqual(-1, ehFeliz);
		}


		[TestMethod]
		public void QuandoPerguntaPeloNumero01_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(1, 2);
			Assert.AreEqual(0, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero10_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(10, 2);
			Assert.AreEqual(1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero130_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(130, 2);
			Assert.AreEqual(2, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero97_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(97, 2);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero49_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(49, 2);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaPeloNumero07_2()
		{
			var numeroFeliz = new NumeroFeliz();
			var ehFeliz = numeroFeliz.PosicaoDaFelicidade(07, 2);
			Assert.AreEqual(-1, ehFeliz);
		}

		[TestMethod]
		public void QuandoPerguntaSeNumerosSaoFelizes()
		{
			var numeroFeliz = new NumeroFeliz();
			Assert.IsTrue(numeroFeliz.EhFeliz(007, 5));
			Assert.IsTrue(numeroFeliz.EhFeliz(049, 4));
			Assert.IsTrue(numeroFeliz.EhFeliz(097, 3));
			Assert.IsTrue(numeroFeliz.EhFeliz(130, 2));
			Assert.IsTrue(numeroFeliz.EhFeliz(010, 1));
			Assert.IsTrue(numeroFeliz.EhFeliz(001, 0));

			Assert.IsFalse(numeroFeliz.EhFeliz(007, 4));
			Assert.IsFalse(numeroFeliz.EhFeliz(049, 3));
			Assert.IsFalse(numeroFeliz.EhFeliz(097, 2));
			Assert.IsFalse(numeroFeliz.EhFeliz(130, 1));
			Assert.IsFalse(numeroFeliz.EhFeliz(010, 0));
			Assert.IsFalse(numeroFeliz.EhFeliz(001, -1));
		}

		[TestMethod]
		public void QuandoPerguntaSeNumerosEhSortudo()
		{
			var numeroFeliz = new NumeroFeliz();
			Assert.IsTrue(numeroFeliz.EhSortudo(15, 0));
			Assert.IsTrue(numeroFeliz.EhSortudo(15, 1));
			Assert.IsTrue(numeroFeliz.EhSortudo(15, 2));
			Assert.IsFalse(numeroFeliz.EhSortudo(15, 3));
			Assert.IsFalse(numeroFeliz.EhSortudo(15, 4));
			Assert.IsFalse(numeroFeliz.EhSortudo(15, 100));
		}
	}
}