using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual void ClearTmp()
		{
			var tmpRoot = GetTmpRoot ();

			Directory.Delete(tmpRoot, true);
		}
	}
}

