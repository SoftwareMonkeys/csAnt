//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CyclePackageScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CyclePackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full package cycle.");
		Console.WriteLine("");

		var packageName = String.Empty;

		if (Arguments.KeylessArguments.Length > 0){
			packageName = Arguments.KeylessArguments[0];
			Console.WriteLine("Package name: " + packageName);
		}

        bool skipIncrement = Arguments.Contains("skipincrement");

        if (skipIncrement)
        {
    		ExecuteScript(
                "EnsureBuild",
                "-skipincrement"
            );
        }
        else
            ExecuteScript("EnsureBuild");
		
		ExecuteScript("ClearBak");

		if (!IsError)
		{
			Console.WriteLine("Creating packages...");
			Console.WriteLine("");

			List<string> execArgs = new List<string>();

			if (packageName != String.Empty)
				execArgs.Add(packageName);

			// Create the package
			ExecuteScript(
				"Package",
				execArgs.ToArray()
			);
		}

		return !IsError;
	}
}
