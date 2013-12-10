using System;
using System.Data;
using System.Data.SqlClient;
using MPSC.Lib.BancoDados;

namespace MPSC.Lib
{
	public class Principal : IExecutavel
	{
		public static void Main(String[] args)
		{
			String opcao = "";
			do
			{
				opcao = MostrarMenu();
				IExecutavel executavel = AbrirProgram(opcao);
				opcao = executavel.Executar();
			}
			while (!opcao.Equals(ConsoleKey.Escape.ToString()));
		}

		private static String MostrarMenu()
		{
			Console.Clear();
			Console.WriteLine("1 - ComoPegarAsMensagensDePrintsDoSqlServer");
			return Console.ReadKey().KeyChar.ToString();
		}

		private static IExecutavel AbrirProgram(String opcao)
		{
			IExecutavel executavel = new Principal();
			switch (opcao)
			{
				case "1":
					executavel = new ComoPegarAsMensagensDePrintsDoSqlServer();
					break;
			}
			return executavel;
		}

		public String Executar()
		{
			Console.WriteLine("Opção inválida");
			return Console.ReadKey().ToString();
		}
	}

	public interface IExecutavel
	{
		String Executar();
	}
}