using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsHelp
		{
			get
			{
				return Arguments.Contains("h")
					|| Arguments.Contains("help");
			}
		}
	}
}

