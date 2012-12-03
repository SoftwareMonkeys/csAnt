using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsHelp
		{
			get
			{
				return Args.Contains("h")
					|| Args.Contains("help");
			}
		}
	}
}

