using System;
using System.Collections.Generic;

namespace MP.Library.CaixaEletronico.Notas
{
	public abstract class Nota : IComparable, IComparable<Nota>
	{
		public readonly Decimal Valor;

		public Nota(Int32 valor)
		{
			Valor = valor;
		}

		public override String ToString()
		{
			return "R$ " + Valor.ToString("0.00"); 
		}

		public IEnumerable<Nota> Clonar(Int32 quantidade)
		{
			for (var i = 0; i < quantidade; i++)
				yield return this;
		}

		public Int32 CompareTo(Object obj)
		{
			return CompareTo(obj as Nota);
		}

		public Int32 CompareTo(Nota nota)
		{
			var retorno = 0;
			if (nota != null) 
			{
				if (Valor > nota.Valor)
					retorno = 1;
				else if (Valor < nota.Valor)
					retorno = -1;
			}
			return retorno;
		}

		public static implicit operator Decimal(Nota nota)
		{
			return nota.Valor;
		}
	}
}