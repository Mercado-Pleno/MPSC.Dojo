namespace MPSC.Library.Exemplos.ControleDeFluxo.Reflection
{
	using System;
	using System.ComponentModel;
	using System.Reflection;
	using System.Xml.Serialization;

	public static class Extensao
	{
		public static String Descricao(this Enum enumerado)
		{
			String descricaoDefault = enumerado.ToString("G");
			FieldInfo fieldInfo = enumerado.GetType().GetField(descricaoDefault);
			Object[] atributos = ((fieldInfo != null) ? fieldInfo.GetCustomAttributes(true) : new Object[] { });
			String descricao = Descricao(atributos, descricaoDefault);
			return String.Format("{0} = {1} ({2})", descricaoDefault, enumerado.ToString("D"), descricao);
		}

		private static String Descricao(Object[] atributos, String descricaoDefault)
		{
			String retorno = descricaoDefault;
			if ((atributos != null) && (atributos.Length > 0))
			{
				var obj = atributos[0];
				if (obj is XmlEnumAttribute)
					retorno = (obj as XmlEnumAttribute).Name;
				else if (obj is DescriptionAttribute)
					retorno = (obj as DescriptionAttribute).Description;
			}
			return retorno;
		}
	}

	[FlagsAttribute]
	public enum Dia
	{
		[XmlEnum("Domingo")]
		Domingo,
		[XmlEnum("Segunda-feira")]
		Segunda,
		[XmlEnum("Terça-feira")]
		Terca,
		[XmlEnum("Quarta-feira")]
		Quarta,
		[XmlEnum("Quinta-feira")]
		Quinta,
		[XmlEnum("Sexta-feira")]
		Sexta,
		[XmlEnum("Sábado")]
		Sabado,
	}

	public class AtributosDescritivosDosEnumerados : IExecutavel
	{
		[FlagsAttribute]
		enum Colors { Red = 1, Green = 2, Blue = 4, Yellow = 8 };
		enum Days { Saturday, Sunday, Monday, Tuesday, Wednesday, Thursday, Friday };
		enum BoilingPoints { Celsius = 100, Fahrenheit = 212 };

		public void Executar()
		{
			Colors myColors = Colors.Red | Colors.Yellow | Colors.Blue;
			Type daysType = typeof(Days);
			Dia dia = (Dia)DateTime.Today.DayOfWeek;
			Dia dias = (Dia)7;

			Console.WriteLine(String.Format("O dia da semana de hoje é {0}.", Enum.Parse(daysType, DateTime.Today.DayOfWeek.ToString(), false)));
			Console.WriteLine(String.Format("O dia da semana de hoje é {0}.", dia.Descricao()));
			Console.WriteLine(String.Format("Os dias disponivesis são {0}.", dias.Descricao()));
			Console.WriteLine(String.Format("O ponto de ebulição Enum defines the following items, and corresponding values:"));
			Console.WriteLine(String.Format("   O ponto de ebulição em graus {0:G} é {0:D}.", BoilingPoints.Celsius));
			Console.WriteLine(String.Format("   O ponto de ebulição em graus {0:G} é {0:D}.", BoilingPoints.Fahrenheit));
			Console.WriteLine(String.Format("myColors está com as seguintes combinações de cores: {0}", myColors));
		}
	}
}