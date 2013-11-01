using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void CreateProjectNode(string projectName)
		{
			var path = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ projectName
					+ ".node";

			var node = new FileNode(
				path,
				new FileNodeSaver()
			);

			node.Save();
		}
	}
}

