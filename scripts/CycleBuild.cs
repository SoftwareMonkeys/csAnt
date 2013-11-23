//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CycleBuildScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleBuildScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full build cycle.");
		Console.WriteLine("");

                var mode = "Release";
                
                var arguments = new Arguments(args);
                
                if (arguments.Contains("mode"))
                        mode = arguments["mode"];
                        
                Console.WriteLine("");
                Console.WriteLine("Build mode: " + mode);
                Console.WriteLine("");

		Console.WriteLine("Cleaning...");
		Console.WriteLine("");

		// Clear the dlls
		ExecuteScript(
			"ClearDlls"
		);

		Console.WriteLine("Building...");
		Console.WriteLine("");

                // If the mode is "all" then build both Debug and Release modes
                if (mode == "all")
                {
		        ExecuteScript(
			        "BuildAllSolutions",
			        "-mode:Release"
		        );
		        
		        ExecuteScript(
			        "BuildAllSolutions",
			        "-mode:Debug"
		        );
		}
		else
		{
		        // Build the solutions
		        ExecuteScript(
			        "BuildAllSolutions",
			        "-mode:" + mode
		        );
		}

		if (!IsError)
		{
			// Copy the binaries to the libraries folder
			ExecuteScript(
				"CopyBinToLib"
			);
		}
		
		return true;
	}
}
