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
            // TODO: Inject mover
            new DirectoryMover().Move(source, target);
		}

	}
}

