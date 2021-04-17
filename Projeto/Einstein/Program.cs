using System;
using System.Threading;

namespace MPSC.Einstein
{
	public class Einstein
	{
		private const string elogio = "Oh meu querido computador que tudo sabes, detentor de todo conhecimento, responda";

		public static void Main(string[] args)
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
					if (letra.Key == ConsoleKey.Backspace)
					{
						Console.Write(" ");
						Console.CursorLeft--;
						resposta = resposta.Substring(0, Console.CursorLeft);
					}
					else
					{
						Console.CursorLeft--;
						resposta += letra.KeyChar;
						Console.Write(elogio.Length >= resposta.Length ? elogio[resposta.Length - 1] : resposta[resposta.Length - 1]);
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
			Calculando();
			Console.WriteLine(resposta + "\r\n\r\n");
			return new ConsoleKeyInfo();
		}

		private static void Calculando()
		{
			Console.CursorVisible = false;
			Console.Write("Procurando resposta ");
			for (int j = 0; j < 3; j++)
			{
				for (int i = 0; i < 40; i++)
				{
					Thread.Sleep(6);
					Console.Write(". ");
				}
				for (int i = 0; i < 40; i++)
				{
					Thread.Sleep(4);
					Console.CursorLeft -= 2;
					Console.Write("  ");
					Console.CursorLeft -= 2;
				}
			}
			Console.CursorLeft = 0;
			Console.Write(new string(' ', 80));
			Console.CursorLeft = 0;
			Console.CursorVisible = true;
		}
	}
}