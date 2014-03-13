using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	public class ControlandoSeOProgramaEstaSendoExecutado : IExecutavel
	{
		public void Executar()
		{
			Console.WriteLine("Esta funcao ja foi acionada: " + Assembly.GetExecutingAssembly().EstaEmExecucao());
		}
	}

	public static class AssemblyExtension
	{
		public static bool EstaEmExecucao(this Assembly assembly)
		{
			return assembly.EstaEmExecucao(false);
		}

		public static bool EstaEmExecucao(this Assembly assembly, Boolean throwExceptions)
		{
			var vRetorno = false;
			bool ownsMutex;
			try
			{
				Mutex mutex = new Mutex(false, assembly.FullName, out ownsMutex);
				if (ownsMutex)
					GC.KeepAlive(mutex);
				else
					throw new InvalidProgramException(assembly.FullName + " já está em execução.");
			}
			catch (InvalidProgramException exception)
			{
				vRetorno = exception.TratarException(throwExceptions);
			}
			catch (UnauthorizedAccessException exception)
			{
				vRetorno = exception.TratarException(throwExceptions);
			}
			catch (IOException exception)
			{
				vRetorno = exception.TratarException(throwExceptions);
			}
			catch (ApplicationException exception)
			{
				vRetorno = exception.TratarException(throwExceptions);
			}
			catch (ArgumentException exception)
			{
				vRetorno = exception.TratarException(throwExceptions);
			}
			return vRetorno;
		}

		private static Boolean TratarException(this Exception exception, Boolean throwExceptions)
		{
			Console.WriteLine(exception.Message);
			GC.Collect();

			if (throwExceptions)
				throw exception;

			return exception is InvalidProgramException;
		}
	}
}