using System;
using System.Collections.Generic;
using System.Linq;

namespace CaixaEletronico
{
	public class Saque
	{
		private List<Nota> notasDisponiveis;

		public Saque()
		{
			notasDisponiveis = new List<Nota>();
			notasDisponiveis.Add(new Nota100());
			notasDisponiveis.Add(new Nota50());
			notasDisponiveis.Add(new Nota20());
			notasDisponiveis.Add(new Nota10());
		}

		public List<Nota> Sacar(int valor)
		{
			List<Nota> notas = new List<Nota>();

			Nota menorNota = MenorNotaDisponivel();
			if (valor < menorNota.Valor)
				throw new ArgumentOutOfRangeException("valor", valor, "O valor do saque não pode ser menor que " + menorNota.ToString());
			else
			{
				int valorRestante = valor;
				Nota nota = MaiorNotaDisponivel();
				while ((valorRestante >= menorNota.Valor) && (nota != null))
				{
					int quantidade = (valorRestante / nota.Valor);
					valorRestante -= (quantidade * nota.Valor);
					notas.AddRange(nota.Clonar(quantidade));

					nota = ObterMaiorNotaMenorQueAtual(nota);
				}

				if (valorRestante != 0)
					throw new ArgumentOutOfRangeException("valor", valor, "O valor do saque não é um múltiplo de " + menorNota.ToString());
			}

			return notas;
		}

		private Nota ObterMaiorNotaMenorQueAtual(Nota nota)
		{
			return notasDisponiveis.Where(n => n.Valor < nota.Valor).Max(n => n.Valor);
		}

		public Nota MenorNotaDisponivel()
		{
			Nota menorNota = notasDisponiveis.Min(n => n.Valor);

			if (menorNota == null)
				throw new ArgumentNullException("NotasDisponiveis", "As cédulas disponiveis para saque não foram inicializadas!");

			return menorNota;
		}

		public Nota MaiorNotaDisponivel()
		{
			Nota maiorNota = notasDisponiveis.Max(n => n.Valor);

			if (maiorNota == null)
				throw new ArgumentNullException("NotasDisponiveis", "As cédulas disponiveis para saque não foram inicializadas!");

			return maiorNota;
		}

	}
}
