using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;


namespace Mongeral.eSim.Common.MemoryCache
{
	public static class AppCache
	{
		internal static readonly Object acesso = new Object();
		internal static readonly Dictionary<Type, ICache> cache = new Dictionary<Type, ICache>();
		static AppCache()
		{
			GC.KeepAlive(acesso);
			GC.KeepAlive(cache);
		}

		public static Int64 RenovarCache()
		{
			var qtd = cache.Sum(c => c.Value.Count);
			cache.Clear();
			Log.Logar(String.Format("Limpando o cache da Aplicação. {0} Objetos removidos.", qtd));
			return qtd;
		}
	}

	public abstract class CacheContainer<TKey, TClasse, TChild> : ICacheContainer<TKey, TClasse>
		where TKey : struct
		where TChild : CacheContainer<TKey, TClasse, TChild>, new()
	{

		protected virtual Cache<TKey, TClasse> CriarPoliticaDeAtualizacaoDoCache(Cache<TKey, TClasse> cache)
		{
			return cache.QueSeRenova.ACada(6).Horas().SeForNoHorarioDe(ExpedienteDaMongeral);
		}
		protected abstract IEnumerable<KeyValuePair<TKey, TClasse>> ObterDadosExternos();
		protected abstract TClasse ObterDadosExternos(TKey key);

		public Cache<TKey, TClasse> ObterCache()
		{
			return ObterCacheOfType(typeof(TChild));
		}

		TClasse ICacheContainer<TKey, TClasse>.Obter(TKey key)
		{
			try
			{
				Log.Logar(String.Format("Load CacheContainer<{0}, {1}, {2}>().Obter({3})", typeof(TKey).Name, name(typeof(TClasse)), typeof(TChild).Name, key));
				return ObterDadosExternos(key);
			}
			catch (Exception exception)
			{
				//exception.Log();
				return default(TClasse);
			}
			//catch (Exception exception)
			//{
			//	var msg = exception.Log("Falha ao Carrgar o Cache", "{756B002E-484D-45fe-A169-28D136162BE3}").Messages;
			//	ExceptionManager.GetInstance().Manage(exception, String.Format("CacheContainer<{0}, {1}, {2}>\r\n{3}", typeof(TKey).Name, name(typeof(TClasse)), typeof(TChild).Name, msg));
			//	return default(TClasse);
			//}
		}

		private Cache<TKey, TClasse> ObterCacheOfType(Type tipo)
		{
			Cache<TKey, TClasse> cache = null;

			if (NaoEstaEmCache(tipo) || CachePrecisaEPodeSerRenovado(tipo))
				cache = RenovarCache();

			if (NaoEstaEmCache(tipo) || CacheFoiRenovado(cache))
				AppCache.cache[tipo] = cache ?? CriarPoliticaDeAtualizacaoDoCache(CreateCache());

			return AppCache.cache[tipo] as Cache<TKey, TClasse>;
		}

		private Cache<TKey, TClasse> RenovarCache()
		{
			var cache = CriarPoliticaDeAtualizacaoDoCache(CreateCache());
			try
			{
				var result = ObterDadosExternos();
				if ((result != null) && result.Any())
				{
					Log.Logar(String.Format("Load CacheContainer<{0}, {1}, {2}>().RenovarCache()", typeof(TKey).Name, typeof(TClasse).Name, typeof(TChild).Name));
					foreach (var item in result)
						cache[item.Key] = item.Value;
				}
			}
			catch (Exception exception)
			{
				//exception.Log();
			}
			//catch (Exception exception)
			//{
			//	var msg = exception.Log("Falha ao Renovar o Cache", "{6637FD8E-FFBB-4d7f-BBC3-A7D6B434CD28}").Messages;
			//	ExceptionManager.GetInstance().Manage(exception, String.Format("CacheContainer<{0}, {1}>\r\n{2}", typeof(TKey).Name, typeof(TClasse).Name, msg));
			//}
			return cache;
		}

		protected KeyValuePair<TKey, TClasse> New(TKey key, TClasse value)
		{
			return new KeyValuePair<TKey, TClasse>(key, value);
		}

		private Cache<TKey, TClasse> CreateCache()
		{
			return new Cache<TKey, TClasse>(this);
		}

		#region // "Membros estáticos"
		public static Cache<TKey, TClasse> Cache { get { return Instancia.ObterCache(); } }
		private static CacheContainer<TKey, TClasse, TChild> _instanciaUnica;
		private static CacheContainer<TKey, TClasse, TChild> Instancia
		{
			get { return (_instanciaUnica ?? (Instancia = new TChild())) as TChild; }
			set
			{
				if (_instanciaUnica == null)
				{
					lock (AppCache.acesso)
					{
						if (_instanciaUnica == null)
							_instanciaUnica = value;
					}
				}
			}
		}
		private static List<Intervalo> ExpedienteDaMongeral
		{
			get
			{
				var expediente = new List<Intervalo>();
				expediente.Add(new Intervalo(DayOfWeek.Monday, DayOfWeek.Friday).Das(08, 30).Ate(12, 00));
				expediente.Add(new Intervalo(DayOfWeek.Monday, DayOfWeek.Friday).Das(13, 00).Ate(18, 30));
				return expediente;
			}
		}

		private static Boolean CacheFoiRenovado(Cache<TKey, TClasse> cache)
		{
			return (cache != null) && (cache.Count > 0);
		}

		private static Boolean CachePrecisaEPodeSerRenovado(Type tipo)
		{
			return AppCache.cache[tipo].EstaExpirado && AppCache.cache[tipo].PodeSerRenovado;
		}

		private static Boolean NaoEstaEmCache(Type tipo)
		{
			return !AppCache.cache.ContainsKey(tipo);
		}

		private static String name(Type type)
		{
			var retorno = type.Name;
			if (type.IsGenericType)
			{
				retorno = retorno.Substring(0, retorno.IndexOf(@"`")) + "<";
				foreach (var tipo in type.GetGenericArguments())
					retorno += name(tipo) + ", ";
				retorno = retorno.Substring(0, retorno.Length - 2) + ">";
			}
			return retorno;
		}

		#endregion // "Membros estáticos"
	}

	public interface ICacheContainer<TKey, TClasse>
	{
		TClasse Obter(TKey key);
	}

	internal static class Log
	{
		private static readonly String _pathLog = @"C:\Transferencia\Log\";

		public static void Logar(String mensagem)
		{
			try
			{
				var horaLog = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
				var path = Path.Combine(_pathLog, String.Format(@"{0}\", DateTime.Today.ToString("yyyy/MM/dd")));
				var arquivoCache = Path.Combine(path, "Cache.Log");
				if (!Directory.Exists(path)) Directory.CreateDirectory(path);

				File.WriteAllText(arquivoCache, horaLog + " - " + mensagem + ReadAll(arquivoCache));
			}
			catch (Exception) { }
		}

		private static String ReadAll(String arquivo)
		{
			try
			{
				return File.Exists(arquivo) ? "\r\n\r\n" + File.ReadAllText(arquivo) : String.Empty;
			}
			catch (Exception) { return String.Empty; }
		}
	}
}