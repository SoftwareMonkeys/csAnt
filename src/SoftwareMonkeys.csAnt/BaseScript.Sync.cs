using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public void Sync(string fromDir, string toDir)
        {
            Sync (fromDir, toDir, "**");
        }
        
        public void Sync (string fromDir, string toDir, params string[] patterns)
        {
            // TODO: Inject component or keep on a property
            new FileSync().Sync(fromDir, toDir, patterns);
        }
	}
}

