using System;
using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IRecurso
	{
		String Nome { get; }
		IList<IServidor> ListaServidor { get; }
		IServidor AdicionarServidor(IServidor servidor);

		IServidor Obter(String Servidor);
	}
}