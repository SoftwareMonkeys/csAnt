using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsMono
		{
			get
			{
		    	return Platform.IsMono;
			}
		}
	}
}

