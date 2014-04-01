using System;
using System.Collections.Generic;
using System.Text;

namespace MP.Library.Aula.Curso
{
	public abstract class NumeroRomano
	{
		public int Valor { get; set; }
		public String Caracter { get; private set; }

		public NumeroRomano(int valor, String caracter)
		{
			Valor = valor;
			Caracter = caracter;
		}

		protected NumeroRomano(NumeroRomano numeroAntes, NumeroRomano numeroDepois)
		{
			Valor = numeroAntes.Valor < numeroDepois.Valor ? numeroDepois.Valor - numeroAntes.Valor : numeroAntes.Valor + numeroDepois.Valor;
			Caracter = numeroAntes.Caracter + numeroDepois.Caracter;
		}
	}

	public class Numero1 : NumeroRomano
	{
		public Numero1() : base(1, "I") { }
	}

	public class Numero2 : NumeroRomano
	{
		public Numero2() : base(new Numero1(), new Numero1()) { }
	}

	public class Numero3 : NumeroRomano
	{
		public Numero3() : base(new Numero2(), new Numero1()) { }
	}

	public class Numero4 : NumeroRomano
	{
		public Numero4() : base(new Numero1(), new Numero5()) { }
	}

	public class Numero5 : NumeroRomano
	{
		public Numero5() : base(5, "V") { }
	}

}