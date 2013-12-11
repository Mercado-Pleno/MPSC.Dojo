namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	using System;
	using System.Collections.Generic;

	public class ValidarTransicoesDeEstado : IExecutavel
	{
		public ItemMenu Executar()
		{
			throw new NotImplementedException();
		}
	}

	public class MaquinaDeEstado<TEstado, TTransicao>
	{
		private Dictionary<String, TEstado> dicionarioEstados = new Dictionary<String, TEstado>();
		private String ObterChave(TEstado estadoOrigem, TTransicao transicao)
		{
			return estadoOrigem.ToString() + "->" + transicao.ToString();
		}

		public TEstado this[TEstado estadoOrigem, TTransicao transicao]
		{
			get
			{
				return Get(estadoOrigem, transicao);
			}
			set
			{
				Add(estadoOrigem, transicao, value);
			}
		}

		private void Add(TEstado estadoOrigem, TTransicao transicao, TEstado estadoFinal)
		{
			if (!dicionarioEstados.ContainsKey(ObterChave(estadoOrigem, transicao)))
				dicionarioEstados.Add(ObterChave(estadoOrigem, transicao), estadoFinal);
			else
				throw new InvalidOperationException("Estado e Transição já existe.");
		}

		private TEstado Get(TEstado estadoOrigem, TTransicao transicao)
		{
			if (!dicionarioEstados.ContainsKey(ObterChave(estadoOrigem, transicao)))
				throw new InvalidOperationException("Estado inválido");
			return dicionarioEstados[ObterChave(estadoOrigem, transicao)];
		}
	}
}