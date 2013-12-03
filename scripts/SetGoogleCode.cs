//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.FileNodes;

class SetGoogleCodeScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SetGoogleCodeScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		EnsureGoogleCodeSecurityNodeExists();
		
		var arguments = new Arguments(args);

		var gcNode = ProjectNode.Nodes["Security"].Nodes["GoogleCode"];

		if (arguments.Contains("Username"))
		{
			gcNode.Properties["Username"] = arguments["Username"];

			Console.WriteLine("Setting 'Username' to: " + gcNode.Properties["Username"]);
		}
		else
			Error("A username must be provided as an argument.");


		if (arguments.Contains("Password"))
		{
			gcNode.Properties["Password"] = arguments["Password"];

			Console.WriteLine("Setting 'Password' (hidden)");
		}
		else
			Error("A password must be provided as an argument.");

		gcNode.Save();
		
		return !IsError;
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
