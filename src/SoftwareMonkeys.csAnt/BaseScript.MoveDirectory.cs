using System;
using System.Collections.Generic;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
        public void MoveDirectory(string source, string target)
        {
            MoveDirectory(source, target, false);
        }

        public void MoveDirectory(string source, string target, bool overwrite)
        {
            // TODO: Inject mover
            new DirectoryMover().Move(source, target, overwrite);
		}

	}
}

