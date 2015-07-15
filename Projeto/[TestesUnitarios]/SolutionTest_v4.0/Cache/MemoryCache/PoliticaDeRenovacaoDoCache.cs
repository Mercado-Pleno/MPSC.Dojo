using System;
using System.Collections.Generic;

namespace MPSC.Library.TestesUnitarios.SolutionTest_v4.Cache.MemoryCache
{
	public interface IRenovacao<TKey, TClasse> where TKey : struct
	{
		IUnidadeDeTempo<TKey, TClasse> ACada(Int32 tempo);
	}

	public interface IUnidadeDeTempo<TKey, TClasse> where TKey : struct
	{
		IHorario<TKey, TClasse> Minutos();
		IHorario<TKey, TClasse> Horas();
		IHorario<TKey, TClasse> Dias();
	}

	public interface IHorario<TKey, TClasse> where TKey : struct
	{
		Cache<TKey, TClasse> SeForNoHorarioDe(IEnumerable<Intervalo> horariosPermitidos);
	}

	public class Intervalo
	{
		private readonly DayOfWeek diaI;
		private readonly DayOfWeek diaF;
		private int horaI = -1;
		private int minutoI = -1;
		private int horaF = -1;
		private int minutoF = -1;

		public Intervalo(DayOfWeek dayOfWeekI, DayOfWeek dayOfWeekF)
		{
			this.diaI = dayOfWeekI;
			this.diaF = dayOfWeekF;
		}

		public Intervalo Das(int horaI, int minutoI)
		{
			this.horaI = horaI;
			this.minutoI = minutoI;
			return this;
		}

		public Intervalo Ate(int horaF, int minutoF)
		{
			this.horaF = horaF;
			this.minutoF = minutoF;
			return this;
		}

		public Boolean Atende()
		{
			return Atende(DateTime.Now);
		}

		public Boolean Atende(DateTime dataHora)
		{
			if ((dataHora.DayOfWeek < diaI) || (dataHora.DayOfWeek > diaF))
				return false;
			var inicio = new DateTime(dataHora.Date.Ticks).AddHours(horaI).AddMinutes(minutoI);
			var termino = new DateTime(dataHora.Date.Ticks).AddHours(horaF).AddMinutes(minutoF);
			return (dataHora >= inicio) && (dataHora < termino);
		}
	}
}