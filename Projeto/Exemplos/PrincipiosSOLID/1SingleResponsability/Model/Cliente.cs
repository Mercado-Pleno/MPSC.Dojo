namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;

	public class Cliente : Entidade
	{
		public String Nome { get; set; }
		public DateTime Nascimento { get; set; }
	}
}