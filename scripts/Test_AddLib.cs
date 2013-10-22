//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;

class Test_AddLibScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_AddLibScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		var libName = "TestLib";

		var dir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ libName;

		var nodePath = dir
			+ Path.DirectorySeparatorChar
			+ libName
			+ ".node";

		// If the node file already exists then delete it
		if (File.Exists(nodePath))
		{
			Directory.Delete(dir, true);
		}

		// If the node already exists in memory then delete it
		if (CurrentNode.Nodes["Libraries"].Nodes.ContainsKey(libName))
		{
			CurrentNode.Nodes["Libraries"].Nodes.Remove(libName);
		}

		ExecuteScript("AddLib", libName, "http://www.nowhere.com/lib.zip");

		if (!Directory.Exists(dir))
		{
			Error("Library directory wasn't created/found.");
		}

		if (!File.Exists(nodePath))
		{
			Error("Library node wasn't created/found.");
		}

		Directory.Delete(dir, true);

		return !IsError;
	}
}
