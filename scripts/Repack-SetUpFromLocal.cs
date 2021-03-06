using System;
using System.IO;
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
            var assemblyFile = "bin/{BuildMode}/csAnt-SetUpFromLocal.exe";

            var arguments = new Arguments(args);
    
            var buildMode = "Release";
            if (arguments.Contains("mode"))
                buildMode = arguments["mode"];

            var dependencies = new string[]{
                "lib/CS-Script.3.7.2.0/lib/net40/CSScriptLibrary.dll",
                "lib/CS-Script.3.7.2.0/lib/net40/Mono.CSharp.dll",
                "lib/Nuget.Core.2.8.1/lib/net40-Client/NuGet.Core.dll",
                "bin/{BuildMode}/SoftwareMonkeys.FileNodes.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Contracts.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.External.Nuget.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.IO.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.IO.Contracts.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Imports.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Processes.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.SetUp.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.SourceControl.Git.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Versions.dll",
                "bin/{BuildMode}/SoftwareMonkeys.FileNodes.dll",
                "bin/{BuildMode}/Newtonsoft.Json.dll"
            };

            new Repacker(assemblyFile, dependencies, buildMode).Repack();

            return !IsError;
	}
}
