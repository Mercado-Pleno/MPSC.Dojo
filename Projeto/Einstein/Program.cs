using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

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


		public static void Main(String[] args)
		{
			var apagador = new ApagadorDeBackup();
			apagador.Apagar();
		}
	}

	public class Einstein
	{
		static String elogio = "Oh meu querido computador que tudo sabes";
		public static void Mains(String[] args)
		{
			var key = new ConsoleKeyInfo();
			while (key.Key != ConsoleKey.Escape)
				key = jogar();
		}

		private static ConsoleKeyInfo jogar()
		{
			ConsoleKeyInfo retorno;
			Console.WriteLine("Digite a sua pergunta:");
			var posicao = 0;
			var resposta = String.Empty;
			var letra = Console.ReadKey();
			if (letra.Key == ConsoleKey.Spacebar)
			{
				Console.CursorLeft--;
				letra = Console.ReadKey();
				while (letra.Key != ConsoleKey.Enter)
				{
					Console.CursorLeft--;
					if (letra.Key != ConsoleKey.Backspace)
					{
						Console.Write(elogio[resposta.Length]);
						resposta += letra.KeyChar;
					}
					else
					{
						Console.Write(" ");
						Console.CursorLeft--;
					}
					posicao = Console.CursorLeft;
					letra = Console.ReadKey();
				}
				Console.CursorLeft = posicao;
				retorno = Responder(resposta);
			}
			else
				retorno = Responder("Só respondo essa pergunta pro meu dono!");

			return retorno;
		}

		private static ConsoleKeyInfo Responder(String resposta)
		{
			Console.ReadLine();
			Console.WriteLine();
			Calculando();
			Console.Write(resposta);
			Console.WriteLine("\r\nPressione alguma tecla para fazer outra pergunta\r\n");
			var retorno = Console.ReadKey();
			Console.WriteLine();
			return retorno;
		}

		private static void Calculando()
		{
			Console.CursorVisible = false;
			Console.Write("Calculando resposta ");
			for (int j = 0; j < 4; j++)
			{
				for (int i = 0; i < 40; i++)
				{
					Thread.Sleep(10);
					Console.Write(".");
				}
				for (int i = 0; i < 40; i++)
				{
					Thread.Sleep(5);
					Console.CursorLeft--;
					Console.Write(" ");
					Console.CursorLeft--;
				}
			}
			Console.WriteLine();
			Console.CursorVisible = true;
		}
	}
}