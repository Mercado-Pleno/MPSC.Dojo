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
		class DualFileInfo
		{
			public readonly FileInfo Origem;
			public readonly FileInfo Destino;

			private DualFileInfo(FileInfo origem, FileInfo destino)
			{
				Origem = origem;
				Destino = destino;
			}

			internal static DualFileInfo Create(FileInfo origem, FileInfo destino)
			{
				return new DualFileInfo(origem, destino);
			}
		}

		class DualDirectoryInfo
		{
			public readonly DirectoryInfo Origem;
			public readonly DirectoryInfo Destino;

			public DualDirectoryInfo(String origem, String destino, params String[] servidor)
			{
				Origem = new DirectoryInfo(origem);
				Destino = new DirectoryInfo(destino);

				if (!Origem.Exists) Origem.Create();
				if (!Destino.Exists) Destino.Create();
			}
		}

		[TestMethod]
		public void QuandoPedePraCopiarSomenteArquivosExistentesNoDestino_Sicoob()
		{
			var eSis = new DualDirectoryInfo(@"D:\Prj\17062\proj-individuais\Schedules\",
				@"\\svdatfs01\Sistemas\bNogueira\Fix17062.Reajuste.eSis.SicApp26\Schedules\",
				@"\\SicApp26\E$\Jobs\eSim\Individual_Fix_Reajuste17062\"
			);

			Copiar(eSis);
		}

		[TestMethod]
		public void QuandoPedePraCopiarSomenteArquivosExistentesNoDestino_Mongeral()
		{
			var eSim = new DualDirectoryInfo(@"D:\Prj\17283\proj-individuais\Schedules\",
				@"\\svdatfs01\Sistemas\bNogueira\Fix17283.Reajuste.eSim.SvDatApp11\Schedules\",
				@"\\SvDatApp11\E$\Jobs\eSim\Individual_Fix.Reajuste17283\"
			);

			Copiar(eSim);
		}

		private static void Copiar(DualDirectoryInfo dir)
		{
			var arquivosOrigem = dir.Origem.GetFiles("*.*", SearchOption.TopDirectoryOnly);
			var arquivosDestino = dir.Destino.GetFiles("*.*", SearchOption.TopDirectoryOnly);

			var arquivos = arquivosOrigem.Join(arquivosDestino, o => o.Name, d => d.Name, DualFileInfo.Create).ToArray();

			foreach (var arquivo in arquivos)
			{
				Console.WriteLine(arquivo.Destino.Name);
				arquivo.Origem.CopyTo(arquivo.Destino.FullName, true);
			}
		}

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