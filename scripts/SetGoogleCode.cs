//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.FileNodes;

class SetGoogleCodeScript : BaseScript
{
	public static void Main(string[] args)
	{
		new SetGoogleCodeScript().Start(new Arguments(args));
	}
	
	public void Start(Arguments args)
	{
		EnsureGoogleCodeSecurityNodeExists();

		var gcNode = ProjectNode.Nodes["Security"].Nodes["GoogleCode"];

		if (args.Contains("Username"))
		{
			gcNode.Properties["Username"] = args["Username"];

			Console.WriteLine("Setting 'Username' to: " + gcNode.Properties["Username"]);
		}


		if (args.Contains("Password"))
		{
			gcNode.Properties["Password"] = args["Password"];

			Console.WriteLine("Setting 'Password' (hidden)");
		}

		gcNode.Save();
	}

	public void EnsureGoogleCodeSecurityNodeExists()
	{
		if (!ProjectNode.Nodes.ContainsKey("Security"))
		{
			CreateSecurityNode();
		}

		if (!ProjectNode.Nodes["Security"].Nodes.ContainsKey("GoogleCode"))
		{
			CreateGoogleCodeNode();
		}
	}

	public void CreateSecurityNode()
	{
		var path = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_security"
			+ Path.DirectorySeparatorChar
			+ "Security.node";

		var node = new FileNode(
			path,
			new FileNodeSaver()
		);

		node.Name = "Security";

		node.Save();

		ProjectNode.Nodes.Add("Security", node);
	}

	public void CreateGoogleCodeNode()
	{
		var path = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "_security"
			+ Path.DirectorySeparatorChar
			+ "GoogleCode"
			+ Path.DirectorySeparatorChar
			+ "GoogleCode .node";

		var node = new FileNode(
			path,
			new FileNodeSaver()
		);

		node.Name = "GoogleCode";

		node.Save();

		ProjectNode.Nodes["GoogleCode"].Nodes.Add("GoogleCode", node);
	}
}
