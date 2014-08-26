using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Service.Util
{
	public static class CloneUtil
	{
		private static Cliente CriarCliente()
		{
			var cliente = new Cliente();
			var coberturaContratada1 = new CoberturaContratada();
			var coberturaContratada2 = new CoberturaContratada();

			coberturaContratada1.Nome = "Teste1";
			coberturaContratada1.Idade = 50;
			coberturaContratada1.CampoQueExisteNosDois = DateTime.Now;

			coberturaContratada2.Nome = "Teste2";
			coberturaContratada2.Idade = 100;
			coberturaContratada2.CampoQueExisteNosDois = DateTime.Today;

			cliente.Nome = "Bruno";
			cliente.Idade = 31;
			cliente.CampoQueSoExisteAqui = DateTime.Today;
			cliente.Coberturas = new List<CoberturaContratada>();
			cliente.Coberturas.Add(coberturaContratada1);
			cliente.Coberturas.Add(coberturaContratada2);

			return cliente;
		}

		public static Object TestarClone()
		{
			var cliente = CriarCliente();
			var gestorCliente = Clone(cliente, typeof(GestorCliente));
			return gestorCliente;
		}

		public static Object Clone(Object obj, Type tipo)
		{
			var retorno = Activator.CreateInstance(tipo);
			return CopiarPropriedades(obj, retorno);
		}

		private static Object CopiarPropriedades(Object source, Object destination)
		{
			var sourceProperties = source.GetType().GetProperties();
			var destinationProperties = destination.GetType().GetProperties();
			foreach (var destinationProperty in destinationProperties)
			{
				if (destinationProperty.CanWrite)
				{
					var sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == destinationProperty.Name);
					if ((sourceProperty != null) && (sourceProperty.CanRead))
					{
						if (PropertyIsCollection(sourceProperty))
							CopiarColecao(source as IEnumerable, destination as IList, destinationProperty, sourceProperty);
						else
							CopiarPropriedade(source, destination, destinationProperty, sourceProperty);
					}
				}
			}
			return destination;
		}

		private static void CopiarPropriedade(Object source, Object destination, System.Reflection.PropertyInfo destinationProperty, System.Reflection.PropertyInfo sourceProperty)
		{
			Object sourceValue = sourceProperty.GetValue(source, null);
			if (destinationProperty.PropertyType != sourceProperty.PropertyType)
				sourceValue = Clone(sourceValue, destinationProperty.PropertyType);
			destinationProperty.SetValue(destination, sourceValue, null);
		}

		private static void CopiarColecao(IEnumerable source, IList destination, System.Reflection.PropertyInfo destinationProperty, System.Reflection.PropertyInfo sourceProperty)
		{
			foreach (var item in source)
			{
				Object sourceValue = item;
				if (destinationProperty.PropertyType != sourceProperty.PropertyType)
					sourceValue = Clone(sourceValue, destinationProperty.PropertyType);

				destination.Add(sourceValue);
			}
		}

		private static bool PropertyIsCollection(System.Reflection.PropertyInfo sourceProperty)
		{
			var pars = sourceProperty.GetIndexParameters();
			return ((pars != null) && (pars.Length > 0));
		}
	}

	class GestorCliente
	{
		public List<GestorCoberturaContratada> Coberturas { get; set; }
		public string Nome { get; set; }
		public int Idade { get; set; }
		public DateTime CampoQueSoExisteLa { get; set; }
	}
	class GestorCoberturaContratada
	{
		public string Nome { get; set; }
		public int Idade { get; set; }
		public DateTime CampoQueExisteNosDois { get; set; }
	}

	class Cliente
	{
		public List<CoberturaContratada> Coberturas { get; set; }
		public string Nome { get; set; }
		public int Idade { get; set; }
		public DateTime CampoQueSoExisteAqui { get; set; }
	}
	class CoberturaContratada
	{
		public string Nome { get; set; }
		public int Idade { get; set; }
		public DateTime CampoQueExisteNosDois { get; set; }
	}

}
