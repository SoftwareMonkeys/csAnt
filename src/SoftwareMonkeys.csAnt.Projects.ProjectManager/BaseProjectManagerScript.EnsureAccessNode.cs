using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.ProjectManager
{
	public partial class BaseProjectManagerScript
	{
		public void EnsureAccessNode()
		{
			if (!CurrentNode.Nodes.ContainsKey("Access"))
			{
				var path = CurrentDirectory
					+ Path.DirectorySeparatorChar
					+ "Access"
					+ Path.DirectorySeparatorChar
					+ "Access.node";

				var node = new FileNode(
					path,
					new FileNodeSaver()
					);

				node.Name = "Access";

				node.Save();
			}
		}
	}
}

