using System;
using System.Collections.Generic;
using System.Threading;
using MPSC.Library.Exemplos;
using System.Linq;
using MPSC.LBJC.Kernel.Delegates;

namespace MPSC.Library.Exemplos.ControleDeFluxo
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
				Thread.Sleep(300);
			}
			Thread.Sleep(2000);
			Console.WriteLine("Finalizado...");
		}

		public void Processar(Object processo)
		{
			var array = (int[])processo;
			Int64 matriculaId = array[1];

			var controle = new ControleExecucao<Int64>();
			controle.ControlarExecucao(ChamarProcedure, matriculaId, random.Next(5000));
		}

		private Object ChamarProcedure(UInt64 threadId, Int64 matriculaId, Object[] args)
		{
			var tempo = Convert.ToInt32(args[0]);
			Console.WriteLine("Processando Thread {0,4}, matricula {1,10} ({2,5} milissegundos)", threadId, matriculaId, tempo);
			Thread.Sleep(tempo);
			Console.WriteLine("Acabou      Thread {0,4}, matricula {1,10}", threadId, matriculaId);
			return null;
		}
	}

	public class ControleExecucao<TKey>
	{
		private static UInt64 threadId = 0;
		private static readonly List<TKey> keys = new List<TKey>();

		public static Boolean PodeExecutar(TKey key)
		{
			lock (keys)
				threadId++;

			var existe = keys.IndexOf(key) >= 0;
			if (!existe)
			{
				lock (keys)
					keys.Add(key);
			}
			return !existe;
		}

		public static void Terminou(TKey key)
		{
			var existe = keys.IndexOf(key) >= 0;
			if (existe)
			{
				lock (keys)
				{
					keys.Remove(key);
				}
			}
		}

		public void ControlarExecucao(Func<UInt64, TKey, Object[], Object> method, TKey key, params Object[] args)
		{
			ControlarExecucao<Object>(method, key, args);
		}

		public TResult ControlarExecucao<TResult>(Func<UInt64, TKey, Object[], TResult> method, TKey key, params Object[] args)
		{
			var result = default(TResult);
			if (PodeExecutar(key))
			{
				try
				{
					result = method(threadId, key, args);
				}
				catch (Exception e)
				{
					Console.WriteLine(e.Message);
				}
				finally
				{
					Terminou(key);
				}
			}
			else
				Console.WriteLine("Erro:       Thread {0,4}, Key       {1,10} Já está sendo executada", threadId, key);
			return result;
		}
	}
}