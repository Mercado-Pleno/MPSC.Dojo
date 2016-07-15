using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.Library.TestesUnitarios.SolutionTest_v4.Exemplos.Job
{
	public class JobBase<T>
	{
		public IJobBase<T> job { get; set; }
		public void Executar()
		{
			var quantidadeDeItensPorLote = job.ObterQuantidadeDeItensPorLote();
			var dados = job.ObterInformacoes();
			var lotes = Fatiar(dados, quantidadeDeItensPorLote);
			foreach (var lote in lotes)
			{
				job.ValidarLote(lote);
				foreach (var item in lote)
				{
					if (job.ValidarItem(item))
						job.ProcessarItem(item);

					job.PosCondicaoItem(item);
				}
				job.PosCondicaoLote(lote);
			}
		}

		private IEnumerable<IEnumerable<T>> Fatiar(IEnumerable<T> dados, Int32 quantidadeDeItensPorLote)
		{
			IEnumerable<T> retorno;
			for (int i = 0; (retorno = dados.Skip(i * quantidadeDeItensPorLote).Take(quantidadeDeItensPorLote)).Any(); i++)
				yield return retorno;
		}
	}

	public interface IJobBase<T>
	{
		Int32 ObterQuantidadeDeItensPorLote();
		IEnumerable<T> ObterInformacoes();
		Boolean ValidarLote(IEnumerable<T> lote);
		Boolean ValidarItem(T n);
		void ProcessarItem(T n);
		void PosCondicaoItem(T n);
		void PosCondicaoLote(IEnumerable<T> lote);
	}
}