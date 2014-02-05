using System;
using MPSC.Library.Exemplos.PrincipiosSOLID._3LiskovSubstitution;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.LiskovSubstitution
{
	public class CalculaImpostoService
	{
		public Decimal CalcularImposto(Produto produto, Imposto imposto)
		{
			return imposto.ObterAliquota() * produto.Preco;
		}
	}


	public class PrincipalImposto
	{
		public void main()
		{
			var produto = new Produto() { Preco = 100 };
			var imposto = new Imposto() { Aliquota = 0.1m };
			var impostoISS = new ISS();
			var impostoICMS = new ICMS() { UF = "RJ" };

			var calculaImpostoService = new CalculaImpostoService();

			produto.ValorImposto = calculaImpostoService.CalcularImposto(produto, impostoICMS);
			produto.ValorImposto = calculaImpostoService.CalcularImposto(produto, imposto);
			produto.ValorImposto = calculaImpostoService.CalcularImposto(produto, impostoISS);

		}
	}
}