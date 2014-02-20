using System;
using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface ITabela { }
	public interface ITabela<Tabela> : ITabela
	{
		String Nome { get; }
		IList<Tabela> ListaDados { get; }
		Tabela Adicionar(Tabela dados);
	}
}