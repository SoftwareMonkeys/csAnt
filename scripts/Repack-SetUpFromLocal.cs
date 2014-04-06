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
            var assemblyFile = "bin/Release/csAnt-SetUpFromLocal.exe";

            var dependencies = new string[]{
                "bin/Release/SoftwareMonkeys.csAnt.External.Nuget.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Imports.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Processes.dll",
                "bin/Release/SoftwareMonkeys.csAnt.SetUp.dll",
                "bin/Release/SoftwareMonkeys.csAnt.SourceControl.Git.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Versions.dll",
                "bin/Release/SoftwareMonkeys.FileNodes.dll",
                "bin/Release/Newtonsoft.Json.dll"
            };

            new Repacker(assemblyFile, dependencies).Repack();

            return !IsError;
	}
}
