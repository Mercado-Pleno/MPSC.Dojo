﻿namespace LBJC
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public abstract class BaseIoC
	{
		protected readonly Dictionary<String, Object> singleton = new Dictionary<String, Object>();
		protected readonly Dictionary<Type, Mapa> dic = new Dictionary<Type, Mapa>();

		protected virtual BaseIoC Add(Type Interface, Type Classe, Boolean singleton, params Object[] parametrosDefault)
		{
			if (Interface == null)
				throw new ArgumentNullException("Interface");

			if (Classe == null)
				throw new ArgumentNullException("Classe");

			if (dic.ContainsKey(Interface))
				throw new ArgumentException(String.Format("A Interface {0} já está mapeada para a classe {1}", Interface.Name, Get(Interface).Type.Name));

			if (!Classe.GetInterfaces().Any(i => i == Interface))
				throw new ArgumentException(String.Format("A Classe {0} não implementa a interface {1}", Classe.Name, Interface.Name));

			if (Classe.IsInterface)
				throw new ArgumentException(String.Format("O Parâmetro {0} deve ser uma classe concreta", Classe.Name));

			if (Classe.IsAbstract)
				throw new ArgumentException(String.Format("A Classe {0} não pode ser abstrata", Classe.Name));

			if (!Classe.GetConstructors().Any(c => c.GetParameters().Length == parametrosDefault.Length))
				throw new ArgumentException(String.Format("Você Precisa definir Parâmetros Default para o construtor da classe {0}", Classe.Name));

			dic.Add(Interface, new Mapa(Classe, singleton, parametrosDefault));

			return this;
		}

		protected virtual Mapa Get(Type type)
		{
			if (!dic.ContainsKey(type))
				throw new ArgumentException("Esta Interface não está mapeada para uma classe");
			return dic[type];
		}

		protected virtual Object New(Type type, params Object[] parametros)
		{
			var vTipoComParametrosDefault = Get(type);
			var vParametros = (parametros.Length > 0) ? parametros : vTipoComParametrosDefault.ParametrosDefault;
			return NewImpl(vTipoComParametrosDefault.Type, vParametros);
		}

		protected virtual Object Singleton(String sessionKey, Type type, params Object[] parametros)
		{
			var vTipoComParametrosDefault = Get(type);
			var vParametros = (parametros.Length > 0) ? parametros : vTipoComParametrosDefault.ParametrosDefault;
			return SingletonImpl(sessionKey, vTipoComParametrosDefault.Type, vParametros);
		}

		protected virtual Object SingletonImpl(String sessionKey, Type type, params Object[] parametros)
		{
			var key = type.FullName + "_" + sessionKey;

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
			protected internal Boolean Singleton { get; private set; }
			protected internal Object[] ParametrosDefault { get; private set; }

			protected internal Mapa(Type type, Boolean singleton, params object[] parametrosDefault)
			{
				Type = type;
				Singleton = singleton;
				ParametrosDefault = parametrosDefault;
			}
		}
	}

	public class IoC : BaseIoC, IIoC
	{
		public virtual IIoC Map<Interface, Classe>(params Object[] parametrosDefault)
			where Interface : class
			where Classe : class
		{
			return Add(typeof(Interface), typeof(Classe), false, parametrosDefault) as IIoC;
		}

		public virtual Type Get<T>() where T : class
		{
			return Get(typeof(T)).Type;
		}

		public virtual T New<T>(params Object[] parametros) where T : class
		{
			return New(typeof(T), parametros) as T;
		}

		public virtual T Singleton<T>(String sessionKey) where T : class
		{
			return SingletonImpl(sessionKey ?? "Master", typeof(T)) as T;
		}
	}
}