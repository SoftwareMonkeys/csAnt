using System;

namespace SoftwareMonkeys.csAnt
{
	public class RequiredArgumentAttribute
	{
		public string ArgumentName { get; set; }

		//public string TypeName { get; set; }

		public RequiredArgumentAttribute (
			string argumentName
		)
		{
			ArgumentName = argumentName;
		}
	}
}

