using System;
using System.Collections.Generic;
using System.Text;

namespace MP.Library.Exemplos.Annotations
{
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	public class TextoAttribute : Attribute
	{
		public int OffSet { get; set; }
		public int Inicio { get; set; }
		public int Tamanho { get; set; }
		public int Fim { get; set; }
		public Type Tipo { get; set; }
		public String Formato { get; set; }

	}
}
