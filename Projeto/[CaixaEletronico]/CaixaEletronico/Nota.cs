using System;
using System.Collections.Generic;

namespace CaixaEletronico
{
	public abstract class Nota : IComparable
	{
		public int Valor { get; private set; }

		public Nota(int valor)
		{
			Valor = valor;
		}

		public override string ToString()
		{
			return "R$ " + Valor.ToString() + ",00"; 
		}

		public List<Nota> Clonar(int quantidade)
		{
			List<Nota> clones = new List<Nota>();
			for (int i = 0; i < quantidade; i++)
			{
				clones.Add(this);
			}

			return clones;
		}

		public int CompareTo(object obj)
		{
			int retorno = 0;
			if ((obj != null) && (obj is Nota))
			{
				Nota nota = obj as Nota;
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