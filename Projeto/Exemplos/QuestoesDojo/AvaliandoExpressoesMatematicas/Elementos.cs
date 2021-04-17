using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public class Elementos : List<string>
	{
		private static readonly char[] Tokens = OperacaoFactory.Operadores.Union(new[] { '(', ')' }).ToArray();

		public bool PrecisaCalcular => Count > 2;

		public override string ToString() => this.Join(" ");

		public Elementos(IEnumerable<string> source) => this.AddRange(source);

		public Elementos(string expressao)
		{
			while (!string.IsNullOrWhiteSpace(expressao))
			{
				var index = expressao.IndexOfAny(Tokens);
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
				var i = 1;
				while (i < Count - 1)
				{
					var exp = Discretizar(this, i);
					if (exp.Antes.All(c => c.In(Tokens)) && exp.Atual.All(c => c.In('+', '-')) && exp.Depois.All(c => Char.IsDigit(c)))
					{
						index = i;
						return true;
					}
					i++;
				}
			}
			return false;
		}

		private (string Antes, string Atual, string Depois) Discretizar(List<string> elementos, int posicao)
		{
			return (elementos[posicao - 1], elementos[posicao], elementos[posicao + 1]);
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



		public void ResolverExpressaoSimples()
		{
			var i = this.IndexOfAny(true, OperacaoFactory.Operadores.Select(o => o.ToString()));
			if (i > 0)
			{
				var exp = Discretizar(this, i);
				var operacao = OperacaoFactory.ObterPor(operador: exp.Atual);
				var valor = operacao.Calcular(numero1: exp.Antes, numero2: exp.Depois);
				this.Simplificar(i - 1, 3, valor);
			}

			this.RemoverParentesesDesnecessarios();
		}

	}
}