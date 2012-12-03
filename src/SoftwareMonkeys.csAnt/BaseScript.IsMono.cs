using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool IsMono
		{
			get
			{
		    	return Type.GetType("Mono.Runtime") != null;
			}
		}
	}
}

