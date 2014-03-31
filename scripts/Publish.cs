using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class PublishScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PublishScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Publishing files...");
		Console.WriteLine("");
	
        // Find all scripts starting with "Publish-"
        var scripts = FindScripts("Publish-*");

        // Loop through the scripts
        foreach (var script in scripts)
            // Execute the script
            ExecuteScript(script);

		return !IsError;
	}
}
