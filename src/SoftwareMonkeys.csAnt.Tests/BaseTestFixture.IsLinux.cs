using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseTestFixture
	{
		// TODO: See if this can be moved to a common utility, so it's not duplicating BaseScript.IsLinux
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
