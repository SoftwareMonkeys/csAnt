using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public virtual string GetTmpRoot()
        {
            return TemporaryDirectoryCreator.GetTmpRoot();
		}
	}
}

