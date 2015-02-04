using System;
using System.IO;
using System.Net;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class SetMyGet : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SetMyGet().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var path = CurrentDirectory + "/_security/Security.node";

		if (!File.Exists (path)) {
			Directory.CreateDirectory (CurrentDirectory + "/_security");
			var node = new FileNode (path, new FileNodeSaver ());

			node.Name = "Security";

			node.Save ();

			CurrentNode.Nodes.Add ("Security", node);

			Console.WriteLine ("Security node created.");
		} else
			Console.WriteLine ("Security node already exists. Skipping creation.");

		return !IsError;
	}
}
