namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;

	public class Produto : Entidade
	{
		public String Nome { get; set; }
		public Decimal Preco { get; set; }
	}
}