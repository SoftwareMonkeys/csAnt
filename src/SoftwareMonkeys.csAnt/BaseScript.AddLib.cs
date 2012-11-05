using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void AddLib (string name, string zipFileUrl)
		{
			EnsureLibsNodeExists();

			CreateLibNode(name, zipFileUrl);
		}

		protected void EnsureLibsNodeExists()
		{
			if (!ProjectNode.Nodes.ContainsKey["Libraries"])
				CreateLibsNode();
		}

		protected void CreateLibsNode()
		{
			var libNodePath = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "Libraries.node";

			// TODO: Check if these should be injected somehow
			var node = new FileNode(
				libNodePath,
				new FileNodeSaver()
			);

			node.Name = "Libraries";

			node.Save ();

			ProjectNode.Nodes.Add ("Libraries", node);
		}
		
		protected void CreateLibNode(string name, string url)
		{
			var libNodePath = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ name
				+ Path.DirectorySeparatorChar
				+ name + ".node";

			// TODO: Check if these should be injected somehow
			var node = new FileNode(
				libNodePath,
				new FileNodeSaver()
			);

			node.Name = name;
			node.Properties["Url"] = url;

			node.Save ();

			ProjectNode.Nodes["Libraries"].Nodes.Add (name, node);
		}
	}
}
