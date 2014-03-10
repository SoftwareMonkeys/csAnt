using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsLinux
		{
		    get
		    {
		        int p = (int) Environment.OSVersion.Platform;
		        return (p == 4) || (p == 6) || (p == 128);
		    }
		}
	}
}

