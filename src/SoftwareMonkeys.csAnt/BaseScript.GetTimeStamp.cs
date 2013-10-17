using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetTimeStamp()
		{
			var dateTime = DateTime.Now;

			return dateTime.Year
				+ "-"
				+ dateTime.Month
				+ "-"
				+ dateTime.Day
				+ "--"
				+ dateTime.Hour
				+ "-"
				+ dateTime.Minute
				+ "-"
				+ dateTime.Second;
		}
	}
}

