using System;
using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IBancoDados
	{
		String Nome { get; }
		IList<ITabela> ListaTabela { get; }
		ITabela<T> AdicionarTabela<T>(ITabela<T> tabela);

		IList<IStoredProcedure> ListaStoredProcedure { get; }
		IStoredProcedure Obter(String nomeStoredProcedure);
	}
}