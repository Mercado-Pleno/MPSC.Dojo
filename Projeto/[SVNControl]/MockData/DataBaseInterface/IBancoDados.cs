using System.Collections.Generic;
using System;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IBancoDados
	{
		String Nome { get; }
		IList<ITabela> ListaTabela { get; }
		ITabela<T> AdicionarTabela<T>(ITabela<T> tabela);
	}
}