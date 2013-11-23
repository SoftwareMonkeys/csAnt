using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTemporaryDirectory()
		{
			return GetTmpDir();
		}

		public virtual string GetTmpDir ()
        {
            return TemporaryDirectoryCreator.GetTmpDir();
		}
	}
}

