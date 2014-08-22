using System;
using System.Collections.Generic;
using System.Text;
using MPSC.Library.Exemplos;
using System.Threading;

namespace MP.Library.Exemplos.ControleDeFluxo
{
	public class ControlandoRecursosCompartilhados : IExecutavel
	{
		private Random random = new Random();
		public void Executar()
		{
			var ts = new ParameterizedThreadStart(o => Processar(o));
			for (int conta = 0; conta < 20; conta++)
			{
				new Thread(ts).Start(new int[] { conta, conta % 4 });
				Thread.Sleep(500);
			}
			Console.WriteLine("Acabou...");
		}

		public void Processar(Object processo)
		{
			var array = (int[])processo;
			var threadId = array[0];
			var matriculaId = array[1];
			var tempo = random.Next(5000);
			try
			{
				if (ControleExecucao.PodeExecutar(matriculaId))
					ChamarProcedure(threadId, matriculaId, tempo);
				else
					Console.WriteLine("Erro:  Thread {0}, matricula {1} Já está sendo executada", threadId, matriculaId);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			finally
			{
				ControleExecucao.Terminou(matriculaId);
			}
		}

		private static void ChamarProcedure(int threadId, int matriculaId, int tempo)
		{
			Console.WriteLine("Processando Thread {0}, matricula {1} ({2} milissegundos)", threadId, matriculaId, tempo);
			Thread.Sleep(tempo);
			Console.WriteLine("Acabou Thread {0}, matricula {1}", threadId, matriculaId);
		}
	}

	public static class ControleExecucao
	{
		private static readonly List<Int64> matriculas = new List<Int64>();

		public static Boolean PodeExecutar(Int64 matricula)
		{
			var existe = matriculas.IndexOf(matricula) >= 0;
			if (!existe)
			{
				lock (matriculas)
				{
					matriculas.Add(matricula);
				}
			}
			return !existe;
		}

		public static void Terminou(Int64 matricula)
		{
			var existe = matriculas.IndexOf(matricula) >= 0;
			if (existe)
			{
				lock (matriculas)
				{
					matriculas.Remove(matricula);
				}
			}
		}
	}
}