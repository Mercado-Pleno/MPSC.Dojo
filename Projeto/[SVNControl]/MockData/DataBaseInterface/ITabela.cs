using System;
using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface ITabela
	{
		String Nome { get; }
	}

	public interface ITabela<Tabela> : ITabela
	{
		IList<Tabela> ListaDados { get; }
		Tabela Adicionar(Tabela dados);
	}
}