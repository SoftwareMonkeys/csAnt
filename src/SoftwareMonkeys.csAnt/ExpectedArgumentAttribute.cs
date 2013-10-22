using System;

namespace SoftwareMonkeys.csAnt
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
	public class ExpectedArgumentAttribute : Attribute
	{
		public string ArgumentName { get; set; }

		//public string TypeName { get; set; }

		public ExpectedArgumentAttribute()
		{}

		public ExpectedArgumentAttribute (
			string argumentName
		)
		{
			ArgumentName = argumentName;
		}
	}
}

