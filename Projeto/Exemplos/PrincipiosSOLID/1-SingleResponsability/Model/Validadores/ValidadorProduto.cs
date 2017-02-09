namespace MPSC.Library.Exemplos.PrincipiosSOLID._1SingleResponsability.Model.Validadores
{
	using System;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Abstracao;
	using MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain;


	public class ValidadorCliente : Validador
	{
		protected override bool ValidarEntidade(Entidade entidade)
		{
			return Validar(entidade as Produto);
		}

		private Boolean Validar(Produto produto)
		{
			if (String.IsNullOrEmpty(produto.Nome))
				throw new ArgumentException("Nome do Produto é nulo");

			if (produto.Preco <= 0)
				throw new ArgumentException("Preço do Produto inválido");

			return true;
		}
	}
}