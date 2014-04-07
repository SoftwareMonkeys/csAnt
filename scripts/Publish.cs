using System;
using System.IO;
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
	
        var packageName = ""; // Empty means all
        if (args.Length > 0)
            packageName = args[0];

        // Find all scripts starting with "Publish-"
        var scripts = FindScripts("Publish-*", packageName);

        // Loop through the scripts
        foreach (var script in scripts)
            // Execute the script
            ExecuteScript(script);

		return !IsError;
	}
}
