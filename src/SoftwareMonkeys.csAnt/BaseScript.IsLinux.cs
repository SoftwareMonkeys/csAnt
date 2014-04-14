using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsLinux
		{
		    get
		    {
		        return Platform.IsLinux;
		    }
		}
	}
}

