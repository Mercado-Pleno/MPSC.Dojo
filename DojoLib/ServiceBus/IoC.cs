using System;
using System.Linq;
using System.Collections.Generic;

namespace LBJC
{
	
	
	
	public class Principal
	{
		public static void Main()
		{
			Container.IoC
				.Map<IPessoa, Pessoa>()
				.Map<IAnimal, Animal>("Gato")
				;
			
			Container.IoC = new Container();
			
			var vObj1 = Container.IoC.New<IPessoa>();
			var vObj2 = Container.IoC.New<IAnimal>();
			var vObj3 = Container.IoC.New<IAnimal>("Cachorro");
			
		}
	}
	
	
	public interface IPessoa
	{
		
	}
	
	public class Pessoa: IPessoa
	{
		
	}
	
	public interface IAnimal
	{
		String Nome { get; set; }
	}
	
	public class Animal: IAnimal
	{
		public String Nome { get; set; }
		public Animal(String nome)
		{
			Nome = nome;
		}
	}
	
	public interface IContainerIoC
	{
		ContainerIoC Map<Interface, Classe>(params Object[] parametros) where Interface : class where Classe: class;
		Type Get<Interface>() where Interface : class;
		Interface New<Interface>(params Object[] parametros) where Interface : class;
	}

	public abstract class ContainerIoC: IContainerIoC
	{
		protected readonly Dictionary<Type, TipoComParametrosDefault> dic;
		public ContainerIoC()
		{
			dic = new Dictionary<Type, TipoComParametrosDefault>();
		}
		
		public virtual ContainerIoC Map<Interface, Classe>(params Object[] parametrosDefault) where Interface : class where Classe: class
		{
			return Add(typeof(Interface), typeof(Classe), parametrosDefault);
		}
		
		public virtual Type Get<T>() where T : class
		{
			return Get(typeof(T)).Type;
		}		

		public virtual T New<T>(params Object[] parametros) where T : class
		{
			return New(typeof(T), parametros) as T;
		}
		
		
		protected virtual ContainerIoC Add(Type Interface, Type classe, params Object[] parametrosDefault)
		{
			if (Interface == null)
				throw new ArgumentNullException("Interface");
			if (classe == null)
				throw new ArgumentNullException("classe");
			if (dic.ContainsKey(Interface))
				throw new ArgumentException("Esta Interface já está mapeada para outra classe");
			if (!classe.GetInterfaces().Any(i => i == Interface))
				throw new ArgumentException("Esta Classe não implementa esta interface");

			dic.Add(Interface, new TipoComParametrosDefault(classe, parametrosDefault));

			return this;
		}
		
		protected virtual TipoComParametrosDefault Get(Type type)
		{
			if (!dic.ContainsKey(type))
				throw new ArgumentException("Esta Interface não está mapeada para uma classe");
			return dic[type];
		}
		
		public virtual Object New(Type type, params Object[] parametros)
		{
			var vTipoComParametrosDefault = Get(type);
			var vParametros = (parametros.Length > 0) ? parametros : vTipoComParametrosDefault.ParametrosDefault;
			return Activator.CreateInstance(vTipoComParametrosDefault.Type, vParametros);
		}
		
		
		protected class TipoComParametrosDefault
		{
			protected internal Type Type { get; private set; }
			protected internal Object[] ParametrosDefault { get; private set; }
			
			protected internal TipoComParametrosDefault(Type type, params object[] parametrosDefault)
			{
				Type = type;
				ParametrosDefault = parametrosDefault;
			}
		}
	}
	
	
	
	
	
	public class Container: ContainerIoC
	{
		private static readonly Object _lock = new Object();
		private static IContainerIoC instancia;
		public static IContainerIoC IoC
		{
			get { return (instancia ?? (IoC = new Container())); }
			set {
				if (PodeAlterar(instancia, value))
				{
					lock (_lock)
					{
						if (PodeAlterar(instancia, value))
							instancia = value;
					}
				}
			}
		}
		
		private static Boolean PodeAlterar(Object obj1, Object obj2)
		{
			return (obj1 == null) || ((obj2 != null) && (obj1 != obj2));
		}
	}
}