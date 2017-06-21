using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Arquivos
{
	[TestClass]
	public class TestandoArquivo
	{
		[TestMethod]
		public void QuandoPedePraJuntarVariosArquivosEmUmSo()
		{
			var diretorioOrigem = new DirectoryInfo(@"C:\Temp\ICATU");
			var arquivoDestino = new FileInfo(@"C:\Temp\SeguradosMigradosICATU.csv");

			JuntarArquivos(diretorioOrigem, arquivoDestino);
		}

		private void JuntarArquivos(DirectoryInfo diretorioOrigem, FileInfo arquivoDestino)
		{
			if (!diretorioOrigem.Exists)
				diretorioOrigem.Create();

			if (arquivoDestino.Exists)
				arquivoDestino.Delete();
			else if (!arquivoDestino.Directory.Exists)
				arquivoDestino.Directory.Create();

			var arquivosOrigem = diretorioOrigem.GetFiles("*.txt", SearchOption.AllDirectories);

			JuntarArquivos_v2(arquivosOrigem, arquivoDestino);
		}

		private void JuntarArquivos_v1(IEnumerable<FileInfo> arquivosOrigem, FileInfo arquivoDestino)
		{
			var cabecalho = 0;
			foreach (var arquivoOrigem in arquivosOrigem)
			{
				CopiarArquivoPara(arquivoOrigem, cabecalho, arquivoDestino);
				cabecalho = 1;
			}
		}

		private static void CopiarArquivoPara(FileInfo arquivoOrigem, Int32 cabecalho, FileInfo arquivoDestino)
		{
			var linhas = File.ReadAllLines(arquivoOrigem.FullName);
			File.AppendAllLines(arquivoDestino.FullName, linhas.Skip(cabecalho));
		}


		#region // "Melhorias do Algoritmo"

		private void JuntarArquivos_v2(IEnumerable<FileInfo> arquivosOrigem, FileInfo arquivoDestino)
		{
			foreach (var arquivoOrigem in arquivosOrigem)
				copiarArquivo(arquivoOrigem, arquivoDestino);
		}
		private static Action<FileInfo, FileInfo> copiarArquivo = CopiarArquivoComCabecalho;
		private static void CopiarArquivoComCabecalho(FileInfo arquivoOrigem, FileInfo arquivoDestino)
		{
			CopiarArquivoPara(arquivoOrigem, 0, arquivoDestino);
			copiarArquivo = CopiarArquivoSemCabecalho;
		}
		private static void CopiarArquivoSemCabecalho(FileInfo arquivoOrigem, FileInfo arquivoDestino)
		{
			CopiarArquivoPara(arquivoOrigem, 1, arquivoDestino);
		}

		#endregion // "Melhorias do Algoritmo"
	}
}