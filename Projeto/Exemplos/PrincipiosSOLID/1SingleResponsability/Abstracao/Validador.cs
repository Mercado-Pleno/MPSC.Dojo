namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain;

	public interface IValidador
	{
		Boolean Validar(Entidade entidade);
	}


	//Aberta para extensoes e fechada para Alterações
	/*
	 A idéia é que quando elaborarmos uma classe, ela tenha que ser
	 * flexivel o suficiente para que qualquer alteração no comportamento dela, não seja necessario
	 * adicionar linhas de código na classe original, e que a alteração no comportamento
	 * possa ser feito em uma classe que a especialize.
	 */
	public abstract class Validador : IValidador
	{
		public Boolean Validar(Entidade entidade)
		{
			return ValidarBasico(entidade) && ValidarEntidade(entidade);
		}

		protected abstract Boolean ValidarEntidade(Entidade entidade);

		protected virtual Boolean ValidarBasico(Entidade entidade)
		{
			if (entidade.Id <= 0)
				throw new ArgumentException("Id da Entidade é nulo");

			return true;
		}
	}
}