using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Exemplos.PrincipiosSOLID.SingleResponsability.Domain
{
	public class Produto: Entidade
	{
		public String Nome { get; set; }
		public Decimal Preco { get; set; }
	}
}
