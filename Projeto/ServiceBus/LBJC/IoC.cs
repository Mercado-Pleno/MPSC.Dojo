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
				throw new KeyNotFoundException(String.Format("A Interface {0} já está mapeada para a Classe {1}", Interface.Name, Get(Interface, false)));

			if (Classe.IsInterface)
				throw new NotSupportedException(String.Format("O Parâmetro {0} deve ser uma Classe Concreta", Classe.Name));

			if (Classe.IsAbstract)
				throw new AccessViolationException(String.Format("A Classe {0} não pode ser Abstrata", Classe.Name));

			if (Classe == Interface)
			{
				if (disparaErroSeInterfaceIgualClasse)
					throw new InvalidOperationException(String.Format("A Classe {0} não pode ser do mesmo tipo da Interface", Classe.Name));
			}
			else if (Classe.GetInterfaces().None(i => i == Interface) && !Classe.IsSubclassOf(Interface))
				throw new InvalidCastException(String.Format("A Classe {0} não {1} {2}", Classe.Name, Interface.IsInterface ? "implementa a Interface" : "é uma SubClasse de", Interface.Name));

			if (Classe.GetConstructors().None(c => c.GetParameters().Length == parametrosDefault.Length))
				throw new EntryPointNotFoundException(String.Format("Você Precisa definir um Construtor sem Parâmetros ou Parâmetros Default para o Construtor da Classe {0}", Classe.Name));

			dic.Add(Interface, new Mapa(Classe, null, parametrosDefault));

			return this;
		}

		protected virtual BaseIoC Add(Type Interface, Object instanciaDeUmaClasseQueImplementaInterface)
		{
			if (Interface == null)
				throw new ArgumentNullException("Interface");

			if (instanciaDeUmaClasseQueImplementaInterface == null)
				throw new ArgumentNullException("objetoOfInterface");

			if (dic.ContainsKey(Interface))
				throw new KeyNotFoundException(String.Format("A Interface {0} já está mapeada para a Classe {1}", Interface.Name, Get(Interface, false)));

			var Classe = instanciaDeUmaClasseQueImplementaInterface.GetType();
			if ((Classe != Interface) && Classe.GetInterfaces().None(i => i == Interface) && !Classe.IsSubclassOf(Interface))
				throw new InvalidCastException(String.Format("A Classe {0} não {1} {2}", Classe.Name, Interface.IsInterface ? "implementa a Interface" : "é uma SubClasse de", Interface.Name));

			dic.Add(Interface, new Mapa(Classe, instanciaDeUmaClasseQueImplementaInterface));

			return this;
		}

		protected virtual Mapa Get(Type type, Boolean disparaErroSeMapeamentoNaoExistir, params Object[] parametros)
		{
			if (!dic.ContainsKey(type))
			{
				if (disparaErroSeMapeamentoNaoExistir)
					throw new InvalidOperationException("Esta Interface ou Classe Abstrata não está mapeada para uma Classe Concreta");
				else
					Add(type, type, disparaErroSeMapeamentoNaoExistir, parametros);
			}
			return dic[type];
		}

		protected virtual Object New(Type type, Boolean disparaErroSeMapeamentoNaoExistir, params Object[] parametros)
		{
			var vMapa = Get(type, disparaErroSeMapeamentoNaoExistir, parametros);
			var vParametros = (parametros.Length > 0) ? parametros : vMapa.ParametrosDefault;
			return vMapa.Instancia ?? NewImpl(vMapa.Type, vParametros);
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
			protected internal Object Instancia { get; private set; }
			protected internal Object[] ParametrosDefault { get; private set; }

			protected internal Mapa(Type type, Object instancia, params object[] parametrosDefault)
			{
				Type = type;
				Instancia = instancia;
				ParametrosDefault = parametrosDefault;
			}
		}
	}

	public class IoC : BaseIoC, IIoC
	{
		private Boolean _disparaErroSeMapeamentoNaoExistir;
		public IoC() : this(false) { }
		protected IoC(Boolean ignoraErroSeMapeamentoNaoExistir) { _disparaErroSeMapeamentoNaoExistir = !ignoraErroSeMapeamentoNaoExistir; }

		public virtual IIoC Map<Interface, Classe>(params Object[] parametrosDefault)
		{
			return Add(typeof(Interface), typeof(Classe), _disparaErroSeMapeamentoNaoExistir, parametrosDefault) as IIoC;
		}

		public virtual IIoC Map<Interface>(Object instancia)
		{
			return Add(typeof(Interface), instancia) as IIoC;
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
		private static IIoC _instancia;

		public static IIoC getIoC(Boolean ignoraErroSeMapeamentoNaoExistir)
		{
			return (_instancia ?? setIoC(new IoC(ignoraErroSeMapeamentoNaoExistir)));
		}

		private static IIoC setIoC(IIoC value)
		{
			if (PodeAlterar(_instancia, value))
			{
				lock (_lock)
				{
					if (PodeAlterar(_instancia, value))
						_instancia = value;
				}
			}
			return _instancia ?? value;
		}

		private static Boolean PodeAlterar(Object obj1, Object obj2)
		{
			return (obj1 == null) || ((obj2 != null) && (obj1 != obj2));
		}
		#endregion // Membros Estáticos
	}
}