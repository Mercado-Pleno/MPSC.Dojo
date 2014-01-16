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
				.Map<IAnimal, Animal>()
				;
			
			//var vObj1 = Container.IoC.New<IPessoa>();
			//var vObj2 = Container.IoC.New<IAnimal>();
			//var vObj3 = Container.IoC.New<IAnimal>("Cachorro");
			
		}
	}
	
	
	public interface IPessoa
	{
		
	}
	
	public class Pessoa: IPessoa
	{
		
	}
	
	public interface IAnimal:IPessoa
	{
		//String Nome { get; set; }
	}
	
	public class Animal: IAnimal
	{
		public String Nome { get; set; }
		public Animal(){}
		public Animal(int a, int b){}
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
		
		
		protected virtual ContainerIoC Add(Type Interface, Type Classe, params Object[] parametrosDefault)
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

			dic.Add(Interface, new TipoComParametrosDefault(Classe, parametrosDefault));

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