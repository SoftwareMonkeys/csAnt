using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetTimeStamp()
		{
			return GetTimeStamp(DateTime.Now);

		}
		
		public string GetTimeStamp(DateTime dateTime)
		{
			var stamp = dateTime.Year
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

			return stamp;
		}
	}
}

