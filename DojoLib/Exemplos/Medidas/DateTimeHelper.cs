namespace MPSC.Library.Exemplos.Medidas
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading;
	using MPSC.Library.Exemplos;


	public class Program : IExecutavel
	{
		public void Executar()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
			//Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

			//Adicionando dias, contemplando apenas os dias �teis.
			DateTime data = DateTime.Now.AddWorkDays(3);

			//Adicionando dias, contemplando apenas dias �teis e definindo uma condi��o
			//que determina se o dia inicial deve estar contemplado no c�lculo.
			//DateTime data = DateTime.Now.AddWorkDays(3, d => d.Hour > 12);

			//Verificando se � ou n�o feriado.
			DateTime temp = new DateTime(2008, 12, 25);
			Console.WriteLine(temp.IsHoliday());
		}
	}

	public static class CultureInfoHelper
	{
		public static DateTime[] GetHolidays(this CultureInfo value, int year)
		{
			List<DateTime> holidays = new List<DateTime>();
			holidays.Add(new DateTime(year, 12, 25));
			holidays.Add(new DateTime(year, 1, 1));

			switch (value.Name)
			{
				case "pt-BR":
					holidays.Add(new DateTime(year, 5, 1));   //Dia do Trabalho
					holidays.Add(new DateTime(year, 7, 9));   //Revolu��o (Somente SP)
					holidays.Add(new DateTime(year, 9, 7));   //Independencia do Brasil
					holidays.Add(new DateTime(year, 11, 2));  //Finados
					holidays.Add(new DateTime(year, 11, 15)); //Proclama��o da Rep�blica
					break;
				case "en-US":
					holidays.Add(new DateTime(year, 7, 4));   //Independencia dos EUA
					break;
			}

			return holidays.ToArray();
		}
	}

	public static class DateTimeHelper
	{
		public static DateTime AddWorkDays(this DateTime value, int quantityOfDaysToAdd)
		{
			return AddWorkDays(value, quantityOfDaysToAdd, true, Thread.CurrentThread.CurrentCulture.GetHolidays(value.Year));
		}

		public static DateTime AddWorkDays(this DateTime value, int quantityOfDaysToAdd, bool startInCurrentDay)
		{
			return AddWorkDays(value, quantityOfDaysToAdd, startInCurrentDay, Thread.CurrentThread.CurrentCulture.GetHolidays(value.Year));
		}

		public static DateTime AddWorkDays(this DateTime value, int quantityOfDaysToAdd, Predicate<DateTime> conditionToStartInCurrentDay)
		{
			return AddWorkDays(value, quantityOfDaysToAdd, conditionToStartInCurrentDay(value), Thread.CurrentThread.CurrentCulture.GetHolidays(value.Year));
		}

		public static DateTime AddWorkDays(this DateTime value, int quantityOfDaysToAdd, Predicate<DateTime> conditionToStartInCurrentDay, DateTime[] holidays)
		{
			return AddWorkDays(value, quantityOfDaysToAdd, conditionToStartInCurrentDay(value), holidays);
		}

		public static DateTime AddWorkDays(this DateTime value, int quantityOfDaysToAdd, bool startInCurrentDay, DateTime[] holidays)
		{
			if (quantityOfDaysToAdd < 0)
				throw new ArgumentOutOfRangeException("quantityOfDaysToAdd", "A quantidade deve ser maior que 0.");

			if (startInCurrentDay)
				value = value.AddDays(-1);

			while (quantityOfDaysToAdd >= 0)
			{
				value = value.AddDays(1);

				if (value.IsHoliday(holidays))
					value = value.AddDays(1);

				if (value.DayOfWeek != DayOfWeek.Sunday && value.DayOfWeek != DayOfWeek.Saturday)
					quantityOfDaysToAdd--;
			}

			return value;
		}

		public static bool IsHoliday(this DateTime value)
		{
			return IsHoliday(value, Thread.CurrentThread.CurrentCulture.GetHolidays(value.Year));
		}

		public static bool IsHoliday(this DateTime value, DateTime[] holidays)
		{
			if (holidays == null || holidays.Length == 0)
				return false;

			return Array.Exists<DateTime>(holidays, d => d == value.Date);
		}
	}
}