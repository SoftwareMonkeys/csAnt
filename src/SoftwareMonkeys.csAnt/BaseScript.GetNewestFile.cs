using System;
using System.IO;
using System.Linq;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string GetNewestFile(string directory)
		{
            return FileNavigator.GetNewestFile(directory);
		}
	}
}

