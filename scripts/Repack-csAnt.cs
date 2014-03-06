using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class Repack_csAntScript : BaseScript
{
	public static void Main(string[] args)
	{
		new Repack_csAntScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            Console.WriteLine("");
            Console.WriteLine("Repacking csAnt.exe file to include dependencies.");
            Console.WriteLine("");
	
            var exeFile = "lib/ILRepack.1.25.0/tools/ILRepack.exe";

            var packedDir = "bin/Release/packed";

            var outFile = packedDir + "/csAnt.exe";
            
            var arguments = new List<string>();

            arguments.Add("/out:" + outFile);
            arguments.Add("/target:exe");
            arguments.Add("/verbose");
            arguments.Add("bin/Release/csAnt.exe");

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
                "lib/FileNodes/bin/Release/SoftwareMonkeys.FileNodes.dll",
                "lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll",
                "lib/Newtonsoft.Json.6.0.1/lib/net40/Newtonsoft.Json.dll",
                "\"lib/cs-script/Lib/Bin/NET 4.0/CSScriptLibrary.dll\"",
                "bin/Release/SoftwareMonkeys.csAnt.Contracts.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll",
                "bin/Release/SoftwareMonkeys.csAnt.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Packages.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Packages.Contracts.dll",
                //"bin/Release/SoftwareMonkeys.csAnt.Projects.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Versions.dll",
                "bin/Release/SoftwareMonkeys.csAnt.SetUp.Common.dll"
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
