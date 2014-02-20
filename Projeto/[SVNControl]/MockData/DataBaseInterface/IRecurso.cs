using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IRecurso
	{
		IList<IServidor> ListaServidor { get; }

		IServidor AdicionarServidor(IServidor servidor);
	}
}