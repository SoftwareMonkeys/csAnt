//css_ref "SoftwareMonkeys.csAnt.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class EnsureNodesScript : BaseScript
{
	public static void Main(string[] args)
	{
		new EnsureNodesScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Ensuring the file nodes exist...");
		Console.WriteLine("");

		Console.WriteLine("Created files:");

		foreach (var csprojFile in Directory.GetFiles(CurrentDirectory, "*.csproj", SearchOption.AllDirectories))
		{
                    var dir = Path.GetDirectoryName(csprojFile);

                    if (Directory.GetFiles(dir, "*.node").Length == 0)
                    {
                        var node = CreateNode(dir, Path.GetFileNameWithoutExtension(csprojFile));

                        node.Properties["Context"] = "Assembly";
                        node.Save();

                        Console.WriteLine(node.FilePath);
                    }
		}

		return !IsError;
	}
}
