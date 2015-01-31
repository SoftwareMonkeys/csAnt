using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public partial class BaseTestFixture
    {
        public string GetTimeStamp()
        {
            if (Time == DateTime.MinValue)
                Time = DateTime.Now;

            return Time.Year
                + "-"
                + Time.Month
                + "-"
                + Time.Day
                + "--"
                + Time.Hour
                + "-"
                + Time.Minute
                + "-"
                + Time.Second
                + "-"
                + Time.Millisecond;
        }
    }
}

