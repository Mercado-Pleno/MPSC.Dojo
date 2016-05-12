using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Library.Exemplos.QuestoesDojo;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.QuestoesDojo
{
	[TestClass]
	public class TestandoPalavrasPrimas
	{
		[TestMethod]
		public void QuandoTestaUmNumero()
		{
			var palavrasPrimas = new PalavrasPrimas();

			Assert.IsTrue(palavrasPrimas.EhPrimo(2));
			Assert.IsTrue(palavrasPrimas.EhPrimo(3));
			Assert.IsTrue(palavrasPrimas.EhPrimo(5));
			Assert.IsTrue(palavrasPrimas.EhPrimo(7));
			Assert.IsTrue(palavrasPrimas.EhPrimo(11));
			Assert.IsTrue(palavrasPrimas.EhPrimo(13));
			Assert.IsTrue(palavrasPrimas.EhPrimo(17));
			Assert.IsTrue(palavrasPrimas.EhPrimo(19));
			Assert.IsTrue(palavrasPrimas.EhPrimo(19));
			Assert.IsTrue(palavrasPrimas.EhPrimo(23));
			Assert.IsTrue(palavrasPrimas.EhPrimo(29));
			Assert.IsTrue(palavrasPrimas.EhPrimo(31));
			Assert.IsTrue(palavrasPrimas.EhPrimo(37));
			Assert.IsTrue(palavrasPrimas.EhPrimo(41));
			Assert.IsTrue(palavrasPrimas.EhPrimo(43));
			Assert.IsTrue(palavrasPrimas.EhPrimo(47));
			Assert.IsTrue(palavrasPrimas.EhPrimo(53));
			Assert.IsTrue(palavrasPrimas.EhPrimo(59));
			Assert.IsTrue(palavrasPrimas.EhPrimo(101));
			Assert.IsTrue(palavrasPrimas.EhPrimo(367));
			Assert.IsTrue(palavrasPrimas.EhPrimo(523));

			Assert.IsFalse(palavrasPrimas.EhPrimo(1));
			Assert.IsFalse(palavrasPrimas.EhPrimo(4));
			Assert.IsFalse(palavrasPrimas.EhPrimo(6));
			Assert.IsFalse(palavrasPrimas.EhPrimo(9));
			Assert.IsFalse(palavrasPrimas.EhPrimo(15));
			Assert.IsFalse(palavrasPrimas.EhPrimo(21));
			Assert.IsFalse(palavrasPrimas.EhPrimo(25));
			Assert.IsFalse(palavrasPrimas.EhPrimo(27));
			Assert.IsFalse(palavrasPrimas.EhPrimo(33));
			Assert.IsFalse(palavrasPrimas.EhPrimo(35));
			Assert.IsFalse(palavrasPrimas.EhPrimo(39));
			Assert.IsFalse(palavrasPrimas.EhPrimo(45));
			Assert.IsFalse(palavrasPrimas.EhPrimo(49));
			Assert.IsFalse(palavrasPrimas.EhPrimo(51));
			Assert.IsFalse(palavrasPrimas.EhPrimo(55));
			Assert.IsFalse(palavrasPrimas.EhPrimo(57));
			Assert.IsFalse(palavrasPrimas.EhPrimo(102));
			Assert.IsFalse(palavrasPrimas.EhPrimo(369));
			Assert.IsFalse(palavrasPrimas.EhPrimo(621));

			Assert.IsFalse(palavrasPrimas.EhPrimo(Int64.MaxValue));
		}
		
		[TestMethod]
		public void QuandoTestaSeEhDivisivel()
		{
			var palavrasPrimas = new PalavrasPrimas();

			Assert.IsFalse(palavrasPrimas.EhDivisivel(0, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(0, 1));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(0, 2));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(0, 3));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(0, 4));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(0, 5));

			Assert.IsFalse(palavrasPrimas.EhDivisivel(1, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(1, 1));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(1, 2));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(1, 3));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(1, 4));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(1, 5));

			Assert.IsFalse(palavrasPrimas.EhDivisivel(2, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(2, 1));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(2, 2));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(2, 3));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(2, 4));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(2, 5));

			Assert.IsFalse(palavrasPrimas.EhDivisivel(3, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(3, 1));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(3, 2));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(3, 3));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(3, 4));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(3, 5));

			Assert.IsFalse(palavrasPrimas.EhDivisivel(4, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(4, 1));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(4, 2));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(4, 3));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(4, 4));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(4, 5));

			Assert.IsFalse(palavrasPrimas.EhDivisivel(5, 0));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(5, 1));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(5, 2));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(5, 3));
			Assert.IsFalse(palavrasPrimas.EhDivisivel(5, 4));
			Assert.IsTrue(palavrasPrimas.EhDivisivel(5, 5));
		}

		[TestMethod]
		public void QuandoTestaUmaLetra()
		{
			var palavrasPrimas = new PalavrasPrimas();

			Assert.AreEqual(00, palavrasPrimas.Quantificar('0'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('1'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('2'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('7'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('8'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('9'));

			Assert.AreEqual(00, palavrasPrimas.Quantificar(' '));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('?'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('!'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar('.'));
			Assert.AreEqual(00, palavrasPrimas.Quantificar(','));
			Assert.AreEqual(00, palavrasPrimas.Quantificar(';'));

			Assert.AreEqual(01, palavrasPrimas.Quantificar('a'));
			Assert.AreEqual(02, palavrasPrimas.Quantificar('b'));
			Assert.AreEqual(03, palavrasPrimas.Quantificar('c'));
			Assert.AreEqual(24, palavrasPrimas.Quantificar('x'));
			Assert.AreEqual(25, palavrasPrimas.Quantificar('y'));
			Assert.AreEqual(26, palavrasPrimas.Quantificar('z'));

			Assert.AreEqual(27, palavrasPrimas.Quantificar('A'));
			Assert.AreEqual(28, palavrasPrimas.Quantificar('B'));
			Assert.AreEqual(29, palavrasPrimas.Quantificar('C'));
			Assert.AreEqual(50, palavrasPrimas.Quantificar('X'));
			Assert.AreEqual(51, palavrasPrimas.Quantificar('Y'));
			Assert.AreEqual(52, palavrasPrimas.Quantificar('Z'));
		}

		[TestMethod]
		public void QuandoTestaUmaPalavra()
		{
			var palavrasPrimas = new PalavrasPrimas();
			Assert.IsFalse(palavrasPrimas.EhPrima(""));
			Assert.IsFalse(palavrasPrimas.EhPrima(" "));
			Assert.IsFalse(palavrasPrimas.EhPrima("Bruno!"));
			Assert.IsFalse(palavrasPrimas.EhPrima("Bruno Fernandes"));

			Assert.IsTrue(palavrasPrimas.EhPrima("b"));
			Assert.IsTrue(palavrasPrimas.EhPrima("Luani"));
		}
	}
}
