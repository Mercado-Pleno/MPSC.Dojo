﻿namespace MPSC.Library.Exemplos.Medidas
{
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System;

	public class DuracaoIntervaloTempoAltaPrecisao : IExecutavel
	{
		public void Executar()
		{
			var performance = new TempoAltaPrecisao();

			Console.WriteLine("Pressione alguma Tecla para Iniciar a contagem");
			Console.ReadKey();
			performance.Start();

			//Thread.Sleep(1000);

			Console.WriteLine("Pressione alguma Tecla para Parar a contagem");
			Console.ReadKey();
			performance.Stop();

			Console.WriteLine("Tempo entre o início e o término: " + performance.Duration);
		}
	}

	public class TempoAltaPrecisao
	{
		private long startTime, stopTime, freq;

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(out long lpFrequency);

		public TempoAltaPrecisao()
		{
			startTime = 0;
			stopTime = 0;
			if (QueryPerformanceFrequency(out freq) == false)
				throw new Win32Exception();
		}

		public void Start()
		{
			QueryPerformanceCounter(out startTime);
		}

		public void Stop()
		{
			QueryPerformanceCounter(out stopTime);
		}

		public double Duration
		{
			get
			{
				return (double)(stopTime - startTime) / (double)freq;
			}
		}
	}
}