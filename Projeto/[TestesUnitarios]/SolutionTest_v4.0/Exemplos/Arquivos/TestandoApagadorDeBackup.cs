using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPSC.Einstein;
using System.IO;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Arquivos
{
	[TestClass]
	public class TestandoApagadorDeBackup
	{
		[TestMethod]
		public void TestMethod1()
		{
			var regex = ApagadorDeBackup.cValidador;
			Assert.IsTrue(regex.IsMatch("2016.06.01_06.00.rar"));
			Assert.IsTrue(regex.IsMatch("2016.05.01_06.00.rar"));
			Assert.IsTrue(regex.IsMatch("2016.06.01_05.00.rar"));
			Assert.IsTrue(regex.IsMatch("2016.06.01_05.01.rar"));
			Assert.IsFalse(regex.IsMatch("2016.06.02_05.01.rar"));
		}

		[TestMethod]
		public void TestMethod2()
		{
			var fileInfo = new FileInfo(@"D:\Prj\MP\DOJO.Projeto\Bin\arquivos\MPSC.Einstein.txt");

			var arquivos = File.ReadAllLines(fileInfo.FullName)
				.Select(l => new FileInfo(Path.Combine(fileInfo.Directory.FullName, l.Substring(3))))
				.ToArray();

			foreach (var arquivo in arquivos)
			{
				if (!arquivo.Directory.Exists)
					arquivo.Directory.Create();

				if (!arquivo.Exists)
					File.WriteAllText(arquivo.FullName, "arquivoFake.rar");
			}
		}

		[TestMethod]
		public void TestMethod3()
		{
			new ApagadorDeBackup().Apagar();
		}
	}
}