using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GetLib(string name)
		{
			GetLib (name, false);
		}

		public void GetLib(string name, bool force)
		{
            new LibraryManager(Nodes.State).Get(name, force);
		}
	}
}
