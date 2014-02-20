using System;
using System.Collections.Generic;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IServidor
	{
		String IP { get; }
		IList<IBancoDados> ListaBancoDados { get; }
		IBancoDados AdicionarBancoDados(IBancoDados bancoDados);

		IBancoDados Obter(String Database);
	}
}