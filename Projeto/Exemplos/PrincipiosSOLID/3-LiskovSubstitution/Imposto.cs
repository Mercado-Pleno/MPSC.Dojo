using System;
using System.Collections.Generic;
using System.Text;

namespace MPSC.Library.Exemplos.PrincipiosSOLID._3LiskovSubstitution
{
	public class Imposto
	{
		public Decimal Aliquota { get; set; }

		public virtual Decimal ObterAliquota()
		{
			return Aliquota;
		}
	}


	public class ISS : Imposto
	{
		public ISS()
		{
			Aliquota = 0.05m;
		}
	}

	public class ICMS : Imposto
	{
		public String UF { get; set; }

		public ICMS()
		{
			Aliquota = (Decimal)0.20;
		}

		public override Decimal ObterAliquota()
		{
			if (UF == "RJ")
				return 0m;
			else
				return base.ObterAliquota();
		}
	}
}