namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain.Validadores
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;

	public class ValidadorCliente : Validador
	{
		protected override bool ValidarEntidade(Entidade entidade)
		{
			return Validar(entidade as Cliente);
		}

		private Boolean Validar(Cliente cliente)
		{
			if (String.IsNullOrEmpty(cliente.Nome))
				throw new ArgumentException("Nome do Cliente é nulo");

			return true;
		}
	}
}