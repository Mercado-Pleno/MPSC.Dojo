namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;

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