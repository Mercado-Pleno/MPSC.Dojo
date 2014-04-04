namespace LBJC
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public abstract class BaseIoC
	{
		protected readonly Dictionary<String, Object> singleton = new Dictionary<String, Object>();
		protected readonly Dictionary<Type, Mapa> dic = new Dictionary<Type, Mapa>();

		protected virtual BaseIoC Add(Type Interface, Type Classe, Boolean disparaErroSeInterfaceIgualClasse, params Object[] parametrosDefault)
		{
			if (Interface == null)
				throw new ArgumentNullException("Interface");

			if (Classe == null)
				throw new ArgumentNullException("Classe");

			if (dic.ContainsKey(Interface))
				throw new ArgumentException(String.Format("A Interface {0} já está mapeada para a classe {1}", Interface.Name, Get(Interface, false)));

			if (Classe.IsInterface)
				throw new ArgumentException(String.Format("O Parâmetro {0} deve ser uma classe concreta", Classe.Name));

			if (Classe.IsAbstract)
				throw new ArgumentException(String.Format("A Classe {0} não pode ser abstrata", Classe.Name));

			if (Classe == Interface)
			{
				if (disparaErroSeInterfaceIgualClasse)
					throw new ArgumentException(String.Format("A classe {0} não pode ser do mesmo tipo da interface", Classe.Name));
			}
			else if (Classe.GetInterfaces().None(i => i == Interface) && !Classe.IsSubclassOf(Interface))
				throw new ArgumentException(String.Format("A Classe {0} não {1} {2}", Classe.Name, Interface.IsInterface ? "implementa a interface" : "é uma SubClasse de", Interface.Name));

			if (Classe.GetConstructors().None(c => c.GetParameters().Length == parametrosDefault.Length))
				throw new ArgumentException(String.Format("Você Precisa definir Parâmetros Default para o construtor da classe {0}", Classe.Name));

			dic.Add(Interface, new Mapa(Classe, parametrosDefault));

			return this;
		}

		protected virtual Mapa Get(Type type, Boolean disparaErroSeMapeamentoNaoExistir, params Object[] parametros)
		{
			if (!dic.ContainsKey(type))
			{
				if (disparaErroSeMapeamentoNaoExistir)
					throw new ArgumentException("Esta Interface não está mapeada para uma classe");
				else
					Add(type, type, disparaErroSeMapeamentoNaoExistir, parametros);
			}
			return dic[type];
		}

		protected virtual Object New(Type type, Boolean disparaErroSeMapeamentoNaoExistir, params Object[] parametros)
		{
			var vMapa = Get(type, disparaErroSeMapeamentoNaoExistir, parametros);
			var vParametros = (parametros.Length > 0) ? parametros : vMapa.ParametrosDefault;
			return NewImpl(vMapa.Type, vParametros);
		}

		protected virtual Object Singleton(String sessionKey, Type type, Boolean disparaErroSeMapeamentoNaoExistir, params Object[] parametros)
		{
			var vTipoComParametrosDefault = Get(type, disparaErroSeMapeamentoNaoExistir, parametros);
			var vParametros = (parametros.Length > 0) ? parametros : vTipoComParametrosDefault.ParametrosDefault;
			return SingletonImpl(sessionKey, vTipoComParametrosDefault.Type, vParametros);
		}

		protected virtual Object SingletonImpl(String sessionKey, Type type, params Object[] parametros)
		{
			var key = type.FullName + "_" + (sessionKey ?? "Master");

			if (!singleton.ContainsKey(key))
			{
				lock (singleton)
				{
					if (!singleton.ContainsKey(key))
						singleton.Add(key, NewImpl(type, parametros));
				}
			}

			return singleton[key];
		}

		protected virtual Object NewImpl(Type type, Object[] parametros)
		{
			return Activator.CreateInstance(type, parametros);
		}

		protected class Mapa
		{
			protected internal Type Type { get; private set; }
			protected internal Object[] ParametrosDefault { get; private set; }

			protected internal Mapa(Type type, params object[] parametrosDefault)
			{
				Type = type;
				ParametrosDefault = parametrosDefault;
			}
		}
	}

	public class IoC : BaseIoC, IIoC
	{
		private Boolean _disparaErroSeMapeamentoNaoExistir;
		public IoC() : this(false) { }
		public IoC(Boolean ignoraErroSeMapeamentoNaoExistir) { _disparaErroSeMapeamentoNaoExistir = !ignoraErroSeMapeamentoNaoExistir; }

		public virtual IIoC Map<Interface, Classe>(params Object[] parametrosDefault)
		{
			return Add(typeof(Interface), typeof(Classe), _disparaErroSeMapeamentoNaoExistir, parametrosDefault) as IIoC;
		}

		public virtual Type Get<T>()
		{
			return Get(typeof(T), _disparaErroSeMapeamentoNaoExistir).Type;
		}

		public virtual T New<T>(params Object[] parametros)
		{
			return (T)New(typeof(T), _disparaErroSeMapeamentoNaoExistir, parametros);
		}

		public virtual T Singleton<T>(String sessionKey, params Object[] parametros)
		{
			return (T)SingletonImpl(sessionKey, typeof(T), _disparaErroSeMapeamentoNaoExistir, parametros);
		}

		#region // Membros Estáticos
		private static readonly Object _lock = new Object();
		private static IIoC instancia;

		public static IIoC getIoC(Boolean ignoraErroSeMapeamentoNaoExistir)
		{
			return (instancia ?? setIoC(new IoC(ignoraErroSeMapeamentoNaoExistir)));
		}

		private static IIoC setIoC(IIoC value)
		{
			if (PodeAlterar(instancia, value))
			{
				lock (_lock)
				{
					if (PodeAlterar(instancia, value))
						instancia = value;
				}
			}
			return instancia ?? value;
		}

		private static Boolean PodeAlterar(Object obj1, Object obj2)
		{
			return (obj1 == null) || ((obj2 != null) && (obj1 != obj2));
		}
		#endregion // Membros Estáticos
	}
}