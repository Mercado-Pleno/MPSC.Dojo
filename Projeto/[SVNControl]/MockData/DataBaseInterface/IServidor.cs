using System.Collections.Generic;
using System;

namespace MP.SVNControl.MockData.DataBaseInterface
{
	public interface IServidor
	{
		String IP { get; }
		IList<IBancoDados> ListaBancoDados { get; }

		IBancoDados AdicionarBancoDados(IBancoDados bancoDados);
	}
}