﻿using System;
using System.Collections.Generic;
﻿using System.Data;
﻿using System.Linq;
using System.Threading;


namespace Mongeral.ESB.Infraestrutura.Base.Containers
{
	public abstract class Container<T>
	{

		private readonly object acesso = new object();

		private List<T> Dados;

		private int cargasRealizadas;
		private Validade validade;
		private bool CarregandoDados;


		public int CargasRealizadas
		{
			get { return cargasRealizadas; }
		}

		private bool NaoCarregado
		{
			get { return Dados == null; }
		}

		private bool Expirou
		{
			get { return validade == null || validade.Expirada; }
		}


		public List<T> ObterTodos()
		{
			VerificarValidade();

			return Dados.ToList();
		}

		protected void VerificarValidade()
		{
			if (NaoCarregado)
			{
				InicializarDados();
				return;
			}

			if (Expirou && !CarregandoDados) //Teste deve ser feito com o acesso "locado".
				lock (acesso)
					if (Expirou && !CarregandoDados) //Teste deve ser feito com o acesso "locado".
						CarregarDadosEmBackGround();
		}

		private void InicializarDados()
		{
			lock (acesso)
			{
				if (NaoCarregado)
					CarregarDados();
			}
		}

		private void CarregarDadosEmBackGround()
		{
			var task = new Thread(CarregarDados) { IsBackground = true };

			task.Start();
		}

		public void ReCarregarDados()
		{
			CarregarDados();
		}

		private void CarregarDados()
		{
			try
			{
				CarregandoDados = true;
				var novos = ColetarDados();

				RenovarDados(novos);
			}
			catch (Exception excecao)
			{
				if (cargasRealizadas == 0)
				{
					throw new Exception(string.Format("Erro na tentativa de carregar os dados do Container '{0}' pela primeira vez.", GetType().FullName), excecao);
				}

				RenovarTempoDeValidade(TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos);

				RegistrarLogDeErro(excecao);
			}
			finally
			{
				CarregandoDados = false;
			}
		}

		protected abstract void RegistrarLogDeErro(Exception excecao);


		private void RenovarTempoDeValidade(long segundos)
		{
			validade = Validade.Nova(segundos);
		}

		private void RenovarDados(List<T> novosDados)
		{
			if (novosDados != null)
			{
				ExecutarRenovacaoDosDados(novosDados);
				ExecutarRenovacaoDosDadosPontoDeExtensao(Dados.ToList());
				RenovarTempoDeValidade(TempoDeValidadeEmSegundos);
			}
			else
			{
				RenovarTempoDeValidade(TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos);
			}
		}

		private void ExecutarRenovacaoDosDados(List<T> novosDados)
		{
			cargasRealizadas++;

			lock (acesso)
			{
				//Console.WriteLine("***RECARREGOU : {0} ***", this.GetType().Name);
				Dados = new List<T>(novosDados);
			}
		}

		protected virtual void ExecutarRenovacaoDosDadosPontoDeExtensao(List<T> novosDados)
		{
		}

		protected abstract List<T> ColetarDados();


		//Valores podem estar definidos em arquivo de configuração.
		public static readonly long UMA_HORA = 60 * 60;
		public static readonly long CINCO_MINUTOS = 60 * 5;

		protected virtual long TempoDeValidadeEmSegundos
		{
			get { return UMA_HORA; }
		}

		protected virtual long TempoDeValidadeDaCargaParaRepeticaoDaColetaEmSegundos
		{
			get { return CINCO_MINUTOS; }
		}

	}
}