using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public class Elementos : List<string>
	{
		public bool PrecisaCalcular => Count > 2;

		public override string ToString() => this.Join(" ");

		public Elementos(IEnumerable<string> source) => this.AddRange(source);

		public Elementos(string expressao)
		{
			expressao = expressao.Replace(" ", "");
			while (!string.IsNullOrWhiteSpace(expressao))
			{
				var index = expressao.IndexOfAny(OperacaoFactory.Tokens);
				if (index == 0)
					index = 1;
				else if (index < 0)
					index = expressao.Length;

				var elemento = expressao.Substring(0, index);
				expressao = expressao.Substring(index);

				if (!string.IsNullOrWhiteSpace(elemento))
					Add(elemento);
			}

			while (TemSinalDuplicado(out var index))
			{
				var sinal = this[index];
				RemoveAt(index);
				this[index] = sinal + this[index];
			}
		}

		private bool TemSinalDuplicado(out int index)
		{
			index = -1;
			if (this.FirstOrDefault().All(c => c == '-'))
			{
				index = 0;
				return true;
			}
			else
			{
				var i = 2;
				while (i < Count)
				{
					if (this[i].All(c => Char.IsDigit(c)) && this[i - 1].All(c => c.In('+', '-')) && this[i - 2].All(c => c.In('^', '*', '/', '%', '+', '-')))
					{
						index = i - 1;
						return true;
					}
					i++;
				}
			}
			return false;
		}

		public void Simplificar(int expressaoStart, int expressaoCount, string expressao)
		{
			RemoveRange(expressaoStart, expressaoCount);
			Insert(expressaoStart, expressao);
		}

		public void Simplificar(int expressaoStart, int expressaoCount, Elementos elementosDentroDoParenteses)
		{
			RemoveRange(expressaoStart, expressaoCount);
			InsertRange(expressaoStart, elementosDentroDoParenteses);
		}

		public void RemoverParentesesDesnecessarios()
		{
			if (this.Count == 3 && this.FirstOrDefault() == "(" && this.LastOrDefault() == ")")
			{
				RemoveAt(0);
				RemoveAt(Count - 1);
			}
		}
	}
}