using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public override void ClearTmp()
		{
			base.ClearTmp();

			var tmpRoot = GetTmpRoot ();

			Directory.Delete(tmpRoot, true);
		}
	}
}

