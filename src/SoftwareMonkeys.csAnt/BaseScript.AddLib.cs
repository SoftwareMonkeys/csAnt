using System;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void AddLib(string name, string zipFileUrl)
		{
			AddLib(name, zipFileUrl, String.Empty);
		}

		public void AddLib (string name, string zipFileUrl, string subPath)
		{
			var cmd = Injection.Retriever.Get<AddLibCommand>(
				new object[]{
					this,
					name,
					zipFileUrl,
					subPath
				}
			);

			cmd.Execute();
		}

		// TODO: Clean up
		/*protected void EnsureLibsNodeExists()
		{
			if (!CurrentNode.Nodes.ContainsKey("Libraries"))
				CreateLibsNode();
		}

		protected void CreateLibsNode()
		{
			var libNodePath = CurrentDirectory
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

			CurrentNode.Nodes.Add ("Libraries", node);
		}
		
		protected void CreateLibNode(string name, string url, string subPath)
		{
			var libNodePath = CurrentDirectory
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
			node.Properties["SubPath"] = subPath;

			node.Save ();

			CurrentNode.Nodes["Libraries"].Nodes.Add (name, node);
		}*/
	}
}

