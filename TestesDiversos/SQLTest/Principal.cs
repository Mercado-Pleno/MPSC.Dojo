namespace MPSC.Lib
{
	using System;
	using System.Data;
	using System.Data.SqlClient;
	using MPSC.Lib.BancoDados;

	public static class Principal
	{
		public static void Main(String[] args)
		{
			byte opcao = 0;
			do
			{
				opcao = MostrarMenu();
				IExecutavel executavel = AbrirProgram(opcao);
				opcao = executavel.Executar(opcao);
			}
			while (opcao != ConsoleKey.Escape.ToKeyCode());
		}

		private static byte MostrarMenu()
		{
			Console.Clear();
			Console.WriteLine("1 - ComoPegarAsMensagensDePrintsDoSqlServer");
			return Console.ReadKey().Key.ToKeyCode();
		}

		private static IExecutavel AbrirProgram(byte opcao)
		{
			IExecutavel executavel = new Padrao();
			switch (opcao.ToConsoleKey())
			{
				case ConsoleKey.NumPad1:
					executavel = new ComoPegarAsMensagensDePrintsDoSqlServer();
					break;
			}
			return executavel;
		}

		public static byte ToKeyCode(this ConsoleKey key)
		{
			return (byte)key;
		}
		public static ConsoleKey ToConsoleKey(this byte key)
		{
			return (ConsoleKey)key;
		}
	}

	public class Padrao : IExecutavel
	{
		public byte Executar(byte key)
		{
			if (key != 27)
			{
				Console.WriteLine(" Opção inválida");
				key = Console.ReadKey().Key.ToKeyCode();
			}
			return key;
		}
	}


	public interface IExecutavel
	{
		byte Executar(byte key);
	}
}