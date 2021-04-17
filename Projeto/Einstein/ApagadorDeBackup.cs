using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MPSC.Einstein
{
	public class ApagadorDeBackup
	{
		public const String cExpressaoDePesquisa = @"\d{4}\.\d{2}\.01_.*\.rar";
		public static readonly Regex cValidador = new Regex(cExpressaoDePesquisa, RegexOptions.Compiled);
		public static readonly IFormatProvider pt_BR = new CultureInfo("pt-BR");
		public static readonly DateTime semanaPassada = DateTime.Today.AddDays(-7);
		public static readonly FileInfo assembly = new FileInfo(Assembly.GetExecutingAssembly().Location);
		public static readonly DirectoryInfo currentDir = assembly.Directory;
		//public static readonly String arquivoTxt = Path.ChangeExtension(assembly.FullName, ".txt");

		public ApagadorDeBackup() { }

		public void Apagar()
		{
			var arquivos = currentDir.GetFiles("*.rar", SearchOption.AllDirectories);
			arquivos = arquivos.Where(a => !cValidador.IsMatch(a.Name)).ToArray();
			arquivos = arquivos.Where(a => GeradoAntesDa(a, semanaPassada)).ToArray();

			foreach (var arquivo in arquivos)
			{
				arquivo.Delete();
				Console.WriteLine("A {0}", arquivo.Name);
				if (!arquivo.Directory.EnumerateFiles().Any())
				{
					arquivo.Directory.Delete();
					Console.WriteLine("D {0}", arquivo.Directory.FullName);
				}
			}

			Console.ReadLine();
		}

		private Boolean GeradoAntesDa(FileInfo a, DateTime semanaPassada)
		{
			try
			{
				var arquivo = Path.GetFileNameWithoutExtension(a.Name);
				arquivo = ((arquivo.Length > 10) ? arquivo.Substring(0, 10) : arquivo);
				var dataGeracao = DateTime.ParseExact(arquivo, "yyyy.MM.dd", pt_BR);
				return dataGeracao <= semanaPassada;
			}
			catch (Exception)
			{
				return false;
			}
		}


		public static void Main2(String[] args)
		{
			var apagador = new ApagadorDeBackup();
			apagador.Apagar();
		}
	}
}