using System;

namespace Timetracker.Extensions
{
	public static class DateTimeExtensions
	{
		
		public static bool SameDay(this DateTime date, DateTime otherDate)
			=> date.DayOfYear == otherDate.DayOfYear && date.Year == otherDate.Year;

	}
}
