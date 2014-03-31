<<<<<<< HEAD
=======
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

>>>>>>> 4feb481a8dbe026e96fb63402b3098b9e6001b42
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
	
<<<<<<< HEAD
        // Find all scripts starting with "Publish-"
        var scripts = FindScripts("Publish-*");

        // Loop through the scripts
        foreach (var script in scripts)
            // Execute the script
            ExecuteScript(script);
=======
		ExecuteScript("PublishSetupFiles");
		ExecuteScript("PublishReleaseZips");
>>>>>>> 4feb481a8dbe026e96fb63402b3098b9e6001b42

		return !IsError;
	}
}
