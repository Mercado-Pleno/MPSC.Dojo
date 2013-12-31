namespace MPSC.Library.Exemplos.ControleDeFluxo.Reflection
{
	using System;
	using System.Reflection;
	using System.Xml.Serialization;

	public static class Extensao
	{
		public static String Descricao(this Enum enumerado)
		{
			FieldInfo fieldInfo = enumerado.GetType().GetField(enumerado.ToString());
			Object[] atributos = ((fieldInfo != null) ? fieldInfo.GetCustomAttributes(true) : new Object[] { });
			String descricao = ((atributos.Length > 0) && (atributos[0] is XmlEnumAttribute)) ? (atributos[0] as XmlEnumAttribute).Name : String.Empty;
			//return String.Format("{0}", enumerado);
			return String.Format("{0} = {1} ({2})", enumerado.ToString("G"), enumerado.ToString("D"), descricao);
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
		Sabado
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

			Console.WriteLine(String.Format("O dia da semana de hoje é {0}.", Enum.Parse(daysType, DateTime.Today.DayOfWeek.ToString(), false)));
			Console.WriteLine(String.Format("O dia da semana de hoje é {0}.", dia.Descricao()));
			Console.WriteLine(String.Format("O ponto de ebulição Enum defines the following items, and corresponding values:"));
			Console.WriteLine(String.Format("   O ponto de ebulição em graus {0:G} é {0:D}.", BoilingPoints.Celsius));
			Console.WriteLine(String.Format("   O ponto de ebulição em graus {0:G} é {0:D}.", BoilingPoints.Fahrenheit));
			Console.WriteLine(String.Format("myColors está com as seguintes combinações de cores: {0}", myColors));
		}
	}
}