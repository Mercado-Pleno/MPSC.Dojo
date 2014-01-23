using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores
{
	public interface IValidador
	{
		Boolean Validar(Entidade entidade);
	}

	public abstract class Validador : IValidador
	{

		public bool Validar(Entidade entidade)
		{
			throw new NotImplementedException();
		}
	}

}
