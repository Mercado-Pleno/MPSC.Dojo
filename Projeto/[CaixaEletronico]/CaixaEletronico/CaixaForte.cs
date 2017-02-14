using MP.Library.CaixaEletronico.Notas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.Library.CaixaEletronico
{
	public class CaixaForte
	{
		private readonly List<Cedulas> cedulas;

		public CaixaForte()
		{
			cedulas = new List<Cedulas>();
		}

		public void Inicializar()
		{
			cedulas.Add(new Cedulas() { Nota = new Nota100(), Quantidade = 1000 });
			cedulas.Add(new Cedulas() { Nota = new Nota050(), Quantidade = 1000 });
			cedulas.Add(new Cedulas() { Nota = new Nota020(), Quantidade = 1000 });
			cedulas.Add(new Cedulas() { Nota = new Nota010(), Quantidade = 1000 });
		}

		public Int32 ObterQuantidadeCedulasDe(Nota nota)
		{
			var notaExistente = cedulas.FirstOrDefault(c => c.Nota.Valor == nota.Valor);
			return (notaExistente == null) ? 0 : notaExistente.Quantidade;
		}

		public void InformarQuantidade(int quantidade, Nota nota)
		{
			var notaExistente = cedulas.FirstOrDefault(c => c.Nota.Valor == nota.Valor);
			if (notaExistente == null)
				cedulas.Add(new Cedulas { Nota = nota, Quantidade = quantidade });
			else
				notaExistente.Quantidade += quantidade;
		}

		public IEnumerable<Cedulas> ObterCedulasDisponiveis()
		{
			return cedulas;
		}
	}
}