using System;
using System.Collections.Generic;

namespace CaixaEletronico
{
	public abstract class Nota : IComparable, IComparable<Nota>
	{
		public Int32 Valor { get; private set; }

		public Nota(Int32 valor)
		{
			Valor = valor;
		}

		public override String ToString()
		{
			return "R$ " + Valor.ToString() + ",00"; 
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
				if (this.Valor > nota.Valor)
					retorno = 1;
				else if (this.Valor < nota.Valor)
					retorno = -1;
			}
			return retorno;
		}

		public static implicit operator Int32(Nota nota)
		{
			return nota.Valor;
		}
	}
}