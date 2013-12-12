namespace MPSC.Library.Exemplos.ControleDeFluxo
{
	using System;
	using System.Collections.Generic;

	public enum Dias { Dom, Seg, Ter, Qua, Qui, Sex, Sab }
	public enum Tempo { Amanha, DepoisDeAmanha, Daqui4Dias, Daqui6Dias }

	public class ValidarTransicoesDeEstado : IExecutavel
	{
		public void Executar()
		{
			var maquinaDeEstado = new MaquinaDeEstado<Dias, Tempo>();
			maquinaDeEstado[Dias.Dom, Tempo.Amanha] = Dias.Seg;
			maquinaDeEstado[Dias.Seg, Tempo.Amanha] = Dias.Ter;
			maquinaDeEstado[Dias.Ter, Tempo.Amanha] = Dias.Qua;
			maquinaDeEstado[Dias.Qua, Tempo.Amanha] = Dias.Qui;
			maquinaDeEstado[Dias.Qui, Tempo.Amanha] = Dias.Sex;
			maquinaDeEstado[Dias.Sex, Tempo.Amanha] = Dias.Sab;
			maquinaDeEstado[Dias.Sab, Tempo.Amanha] = Dias.Dom;

			maquinaDeEstado[Dias.Dom, Tempo.DepoisDeAmanha] = Dias.Ter;
			maquinaDeEstado[Dias.Seg, Tempo.DepoisDeAmanha] = Dias.Qua;
			maquinaDeEstado[Dias.Ter, Tempo.DepoisDeAmanha] = Dias.Qui;
			maquinaDeEstado[Dias.Qua, Tempo.DepoisDeAmanha] = Dias.Sex;
			maquinaDeEstado[Dias.Qui, Tempo.DepoisDeAmanha] = Dias.Sab;
			maquinaDeEstado[Dias.Sex, Tempo.DepoisDeAmanha] = Dias.Dom;
			maquinaDeEstado[Dias.Sab, Tempo.DepoisDeAmanha] = Dias.Seg;

			maquinaDeEstado[Dias.Dom, Tempo.Daqui4Dias] = Dias.Qui;
			maquinaDeEstado[Dias.Seg, Tempo.Daqui4Dias] = Dias.Sex;
			maquinaDeEstado[Dias.Ter, Tempo.Daqui4Dias] = Dias.Sab;
			maquinaDeEstado[Dias.Qua, Tempo.Daqui4Dias] = Dias.Dom;
			maquinaDeEstado[Dias.Qui, Tempo.Daqui4Dias] = Dias.Seg;
			maquinaDeEstado[Dias.Sex, Tempo.Daqui4Dias] = Dias.Ter;
			maquinaDeEstado[Dias.Sab, Tempo.Daqui4Dias] = Dias.Qua;
															   
			maquinaDeEstado[Dias.Dom, Tempo.Daqui6Dias] = Dias.Sab;
			maquinaDeEstado[Dias.Seg, Tempo.Daqui6Dias] = Dias.Dom;
			maquinaDeEstado[Dias.Ter, Tempo.Daqui6Dias] = Dias.Seg;
			maquinaDeEstado[Dias.Qua, Tempo.Daqui6Dias] = Dias.Ter;
			maquinaDeEstado[Dias.Qui, Tempo.Daqui6Dias] = Dias.Qua;
			maquinaDeEstado[Dias.Sex, Tempo.Daqui6Dias] = Dias.Qui;
			maquinaDeEstado[Dias.Sab, Tempo.Daqui6Dias] = Dias.Sex;
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