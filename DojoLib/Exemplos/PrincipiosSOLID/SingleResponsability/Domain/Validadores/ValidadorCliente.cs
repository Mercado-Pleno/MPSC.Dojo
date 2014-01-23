using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores
{
	public class ValidadorCliente : Validador
	{
		public Boolean Validar(Cliente cliente)
		{
			if (String.IsNullOrEmpty(cliente.Nome))
				throw new ArgumentException("Nome do Cliente é nulo");

			if (cliente.Id <= 0)
				throw new ArgumentException("Id do Cliente é nulo");

			return true;
		}
	}
}
