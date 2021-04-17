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
			var expressao = Padronizar(formula);
			var elementos = new Elementos(expressao);
			var resultado = ResolverExpressao(elementos);
			return Convert.ToDecimal(resultado);
		}

		private string ResolverExpressao(Elementos elementos)
		{
			DoResolver("f(x) =", elementos);
			var expressao = new Expressao(elementos);
			while (expressao.PossuiParenteses)
			{
				expressao.ElementosDentroDoParenteses.ResolverExpressaoSimples();
				elementos.Simplificar(expressao.Start, expressao.Count, expressao.ElementosDentroDoParenteses);
				DoResolver("=>", elementos);
				expressao = new Expressao(elementos);
			}

			while (elementos.PrecisaCalcular)
			{
				elementos.ResolverExpressaoSimples();
				DoResolver("=>", elementos);
			}

			return elementos.First();
		}
		public string Padronizar(string expressao)
		{
			var retorno = expressao.Replace(" ", "");
			retorno = retorno.Replace("{", "(");
			retorno = retorno.Replace("[", "(");
			retorno = retorno.Replace("}", ")");
			retorno = retorno.Replace("]", ")");
			return retorno;
		}

	}
}