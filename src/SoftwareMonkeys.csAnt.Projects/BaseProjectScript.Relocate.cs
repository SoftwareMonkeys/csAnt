using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		/// <summary>
		/// Changes the CurrentDirectory to the new location and updates other properties.
		/// </summary>
		public void Relocate(string newLocationPath)
		{
			CreateProjectNode(Path.GetFileName(newLocationPath));

            base.Relocate(newLocationPath);
		}
	}
}

