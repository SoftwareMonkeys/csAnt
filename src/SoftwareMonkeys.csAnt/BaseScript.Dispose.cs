using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void Dispose()
		{
			ClearTmp();
		}
	}
}

