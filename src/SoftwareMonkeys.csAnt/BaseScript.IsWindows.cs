using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsWindows
		{
			get
			{
				return (Environment.OSVersion.Platform == PlatformID.Win32NT);
			}
		}
	}
}

