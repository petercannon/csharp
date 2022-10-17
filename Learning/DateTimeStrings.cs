using System;
					
public class Program
{
	public static void Main()
	{
		var timeStringLong = "2020-03-25T18:30:00";
		var parsedDateLong = DateTime.Parse(timeStringLong);
		var timeStringShort = "2020-03-25";
		var parsedDateShort = DateTime.Parse(timeStringShort);
		Console.WriteLine(parsedDateLong);
		Console.WriteLine(parsedDateShort);
		var longDateFormattedString = DateTime.Parse(timeStringLong).ToString("dd/MM/yyyy HH:mm:ss");
		var shortDateTimeFormattedString = DateTime.Parse(timeStringShort).ToString("dd/MM/yyyy");
		Console.WriteLine(longDateFormattedString);
		Console.WriteLine(shortDateTimeFormattedString);
	}
}
