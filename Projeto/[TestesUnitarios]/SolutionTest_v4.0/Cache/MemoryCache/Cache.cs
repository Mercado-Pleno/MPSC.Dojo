using System;
using System.Collections.Generic;
using System.Linq;

namespace MPSC.Library.TestesUnitarios.SolutionTest_v4.Cache.MemoryCache
{
	public interface ICache
	{
		Boolean EstaExpirado { get; }
		Boolean PodeSerRenovado { get; }
		Int32 Count { get; }
	}

	public class Cache<TKey, TClasse> : ICache, IRenovacao<TKey, TClasse>, IUnidadeDeTempo<TKey, TClasse>, IHorario<TKey, TClasse> where TKey : struct
	{
		private readonly Dictionary<TKey, TClasse> _cache = new Dictionary<TKey, TClasse>();
		private int _tempo;
		private DateTime _expiraEm = DateTime.Now;
		private IEnumerable<Intervalo> _horariosPermitidos;
		private readonly ICacheContainer<TKey, TClasse> _cacheContainer;

		public Boolean EstaExpirado { get { return (_expiraEm < DateTime.Now) || (Count == 0); } }
		public Boolean PodeSerRenovado { get { return _horariosPermitidos.Any(h => h.Atende()) || (_horariosPermitidos.Count() == 0) || (Count == 0); } }
		public Int32 Count { get { return _cache.Count; } }
		public TClasse this[TKey key] { set { _cache[key] = value; } }
		internal Cache(ICacheContainer<TKey, TClasse> cacheContainer)
		{
			_cacheContainer = cacheContainer;
		}

		public TClasse Obter(TKey key)
		{
			return _cache.ContainsKey(key) ? _cache[key] : ObterDadosExternos(key);
		}

		public IEnumerable<TClasse> ObterTodos(Func<TClasse, Boolean> filtro)
		{
			return _cache.Values.Where(c => filtro(c));
		}

		public IEnumerable<TClasse> ObterTodos()
		{
			return _cache.Values;
		}

		private TClasse ObterDadosExternos(TKey key)
		{
			TClasse classe = _cacheContainer.Obter(key);
			if (classe != null) _cache[key] = classe;
			return classe;
		}

		#region // "IRenovacao"

		public IRenovacao<TKey, TClasse> QueSeRenova { get { return this; } }

		IUnidadeDeTempo<TKey, TClasse> IRenovacao<TKey, TClasse>.ACada(Int32 tempo)
		{
			_tempo = tempo;
			return this;
		}

		#endregion // "IRenovacao"

		#region // "IUnidadeDeTempo"

		IHorario<TKey, TClasse> IUnidadeDeTempo<TKey, TClasse>.Minutos()
		{
			_expiraEm = DateTime.Now.AddMinutes(_tempo);
			return this;
		}
		IHorario<TKey, TClasse> IUnidadeDeTempo<TKey, TClasse>.Horas()
		{
			_expiraEm = DateTime.Now.AddHours(_tempo);
			return this;
		}
		IHorario<TKey, TClasse> IUnidadeDeTempo<TKey, TClasse>.Dias()
		{
			_expiraEm = DateTime.Now.AddDays(_tempo);
			return this;
		}

		#endregion // "IUnidadeDeTempo"

		#region // "IHorario"

		Cache<TKey, TClasse> IHorario<TKey, TClasse>.SeForNoHorarioDe(IEnumerable<Intervalo> horariosPermitidos)
		{
			_horariosPermitidos = horariosPermitidos;
			return this;
		}

		#endregion // "IHorario"
	}
}