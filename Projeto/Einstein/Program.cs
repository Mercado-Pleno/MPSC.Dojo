using System;
using System.Threading;

namespace MPSC.Einstein
{
	public class Einstein
	{
		static String elogio = "Oh meu querido computador que tudo sabes";
		public static void Main(String[] args)
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