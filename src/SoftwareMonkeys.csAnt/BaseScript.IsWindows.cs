using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsWindows
		{
			get
			{
				return Platform.IsWindows;
			}
		}
	}
}
