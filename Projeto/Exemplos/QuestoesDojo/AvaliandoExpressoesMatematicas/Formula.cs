using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.Exemplos.QuestoesDojo.AvaliandoExpressoesMatematicas
{
	public delegate void OnResolver(string passo, IEnumerable<string> elementos);
	public class Formula
	{
		public OnResolver OnResolver { get; set; }
		private void DoResolver(string passo, IEnumerable<string> elementos) => OnResolver?.Invoke(passo, elementos);

		public decimal Calcular(string formula)
		{
			var elementos = new Elementos(formula);
			var resultado = ResolverExpressao(elementos);
			return Convert.ToDecimal(resultado);
		}

		private string ResolverExpressao(Elementos elementos)
		{
			DoResolver("f(x) =", elementos);
			var expressao = new Expressao(elementos);
			while (expressao.PossuiParenteses)
			{
				ResolverExpressaoSimples(expressao.ElementosDentroDoParenteses);
				elementos.Simplificar(expressao.Start, expressao.Count, expressao.ElementosDentroDoParenteses);
				DoResolver("=>", elementos);
				expressao = new Expressao(elementos);
			}

			while (elementos.PrecisaCalcular)
			{
				ResolverExpressaoSimples(elementos);
				DoResolver("=>", elementos);
			}

			return elementos.First();
		}

		private void ResolverExpressaoSimples(Elementos elementos)
		{
			var i = elementos.IndexOfAny(true, "*", "/", "^", "%", "+", "-");
			if (i > 0)
			{
				var operacao = OperacaoFactory.ObterPor(operador: elementos[i]);
				var valor = operacao.Calcular(numero1: elementos[i - 1], numero2: elementos[i + 1]);
				elementos.Simplificar(i - 1, 3, valor);
			}

			elementos.RemoverParentesesDesnecessarios();
		}
	}
}