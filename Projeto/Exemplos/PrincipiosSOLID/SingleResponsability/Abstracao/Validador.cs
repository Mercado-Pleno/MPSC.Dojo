namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao
{
	using System;

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