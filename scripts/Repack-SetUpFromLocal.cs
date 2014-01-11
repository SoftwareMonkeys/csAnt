//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class Repack_SetUpFromLocalScript : BaseScript
{
	public static void Main(string[] args)
	{
		new Repack_SetUpFromLocalScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            Console.WriteLine("");
            Console.WriteLine("Repacking SetUpFromLocal.exe file to include dependencies.");
            Console.WriteLine("");
	
            var exeFile = "lib/ILRepack.1.23.0/tools/ILRepack.exe";

            var packedDir = "bin/Release/Packed";

            var outFile = packedDir + "/SetUpFromLocal.exe";
            
            var arguments = new List<string>();

            arguments.Add("/out:" + outFile);
            arguments.Add("/target:exe");
            arguments.Add("/verbose");
            arguments.Add("bin/Release/SetUpFromLocal.exe");

            AddDependencies(arguments);
            
            Console.WriteLine("Output:");
            Console.WriteLine(outFile);
            Console.WriteLine("");

            EnsureDirectoryExists(ToAbsolute(packedDir));
            
            StartDotNetExe(exeFile, arguments.ToArray());

            Console.WriteLine("Done");

            return !IsError;
	}

	public void AddDependencies(List<string> arguments)
	{
            var dependencies = new string[]{
                "bin/Release/SoftwareMonkeys.csAnt.IO.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll"
            };

            Console.WriteLine("Dependencies:");

            foreach (var dependency in dependencies)
            {
                arguments.Add(dependency);

                Console.WriteLine("  " + dependency);
            }

            Console.WriteLine("");
	}
}
