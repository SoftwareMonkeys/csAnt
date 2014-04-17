//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll

using System;
using System.IO;
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

        bool skipIncrement = Arguments.ContainsKey("skipincrement");
                
        Console.WriteLine("");
        Console.WriteLine("Build mode: " + mode);
        Console.WriteLine("");

		Console.WriteLine("Cleaning...");
		Console.WriteLine("");

		// Clear the dlls
		ExecuteScript(
			"ClearDlls"
		);

        if (!skipIncrement)
        {
		    // Increment the version
		    ExecuteScript(
			    "IncrementVersion"
		    );
        }

		// Increment the version
		ExecuteScript(
			"GenerateAssemblyInfoFiles"
		);

		Console.WriteLine("Building...");
		Console.WriteLine("");

        var solutions = new string[]{};

        if (CurrentNode.Properties.ContainsKey("CycleBuildSolutions"))
        {
            solutions = CurrentNode.Properties["CycleBuildSolutions"].Split(',');
        }

        // TODO: Better organize the following code so it's shorter
        // If the mode is "all" then build both Debug and Release modes
        if (mode == "all")
        {
                if (solutions.Length > 0)
                {
                    foreach (var solution in solutions)
                    {
                        ExecuteScript("BuildSolution", solution, "-mode=Debug");
                        ExecuteScript("BuildSolution", solution, "-mode=Release");
                    }
                    
                }
                else
                {
		            ExecuteScript(
			            "BuildAllSolutions",
			            "-mode=Debug"
		            );

		            ExecuteScript(
			            "BuildAllSolutions",
			            "-mode=Release"
		            );
                }
		}
		else
		{
                if (solutions.Length > 0)
                {
                    foreach (var solution in solutions)
                    {
                        ExecuteScript("BuildSolution", solution, "-mode=" + mode);
                    }
                }
                else
                {
	                // Build the solutions
	                ExecuteScript(
		                "BuildAllSolutions",
		                "-mode=" + mode
	                );
                }
		}

		if (!IsError)
		{
            ExecuteScript("Repack", "-mode=" + mode);
		}

		if (!IsError)
		{
			RaiseEvent("Build");
		}
		
		return true;
	}
}
