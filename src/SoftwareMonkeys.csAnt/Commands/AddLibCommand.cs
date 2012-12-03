using System;

namespace SoftwareMonkeys.csAnt
{
	public class AddLibCommand : BaseScriptCommand
	{
		public IScript Script { get;set; }

		public string Name { get;set; }

		public string ZipFileUrl { get;set; }

		public string SubPath { get;set; }

		public AddLibCommand(
			IScript script,
			string name,
			string zipFileUrl
		)
		{
			Name = name;

			ZipFileUrl = zipFileUrl;
		}
		
		public AddLibCommand(
			IScript script,
			string name,
			string zipFileUrl,
			string subPath
		)
		{
			Name = name;

			ZipFileUrl = zipFileUrl;

			SubPath = subPath;
		}

		public override void Execute()
		{
			AddLib(
				Name,
				ZipFileUrl,
				SubPath
			);
		}
		
		public void AddLib(string name, string zipFileUrl)
		{
			AddLib(name, zipFileUrl, String.Empty);
		}

		public void AddLib (string name, string zipFileUrl, string subPath)
		{
			EnsureLibsNodeExists();

			CreateLibNode(name, zipFileUrl, subPath);
		}

		protected void EnsureLibsNodeExists()
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
		}
	}
}

