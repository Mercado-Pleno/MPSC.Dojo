﻿using MP.Library.CaixaEletronico;
using MP.Library.CaixaEletronico.Notas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.Library.CaixaEletronico
{
	public class Saque
	{
		private CaixaForte caixaForte;
		private IEnumerable<Cedulas> cedulasDisponiveis { get { return caixaForte.ObterCedulasDisponiveis(); } }

		public Saque(CaixaForte caixaForte)
		{
			this.caixaForte = caixaForte;
		}

		public List<Nota> Sacar(Decimal valorSolicitado)
		{
			var notasEntregues = new List<Nota>();

			var menorNota = MenorNotaDisponivel();
			if (valorSolicitado < menorNota.Valor)
				throw new SaqueException(String.Format("O valor do saque R$ {0:0.00} não pode ser menor que {1}", valorSolicitado, menorNota.ToString()));
			else
			{
				var valorRestante = valorSolicitado;
				var nota = MaiorNotaDisponivel();
				while ((valorRestante >= menorNota.Valor) && (nota != null))
				{
					var quantidade = Convert.ToInt32(valorRestante) / Convert.ToInt32(nota.Valor);
					valorRestante -= (quantidade * nota.Valor);
					notasEntregues.AddRange(nota.Clonar(quantidade));

					nota = ObterMaiorNotaMenorQueAtual(nota);
				}

				if (valorRestante != Decimal.Zero)
					throw new SaqueException(String.Format("O valor do saque {0} não é um múltiplo de {1}", valorSolicitado, menorNota.ToString()));
			}

			return notasEntregues;
		}

		private Nota ObterMaiorNotaMenorQueAtual(Nota nota)
		{
			return cedulasDisponiveis.Where(c => c.Nota.Valor < nota.Valor).Max(c => c.Nota);
		}

		public Nota MenorNotaDisponivel()
		{
			Nota menorNota = cedulasDisponiveis.Min(c => c.Nota);

			if (menorNota == null)
				throw new ArgumentNullException("NotasDisponiveis", "As cédulas disponiveis para saque não foram inicializadas!");

			return menorNota;
		}

		public Nota MaiorNotaDisponivel()
		{
			Nota maiorNota = cedulasDisponiveis.Max(c => c.Nota);

			if (maiorNota == null)
				throw new ArgumentNullException("NotasDisponiveis", "As cédulas disponiveis para saque não foram inicializadas!");

			return maiorNota;
		}

	} 
}