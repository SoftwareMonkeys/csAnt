using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;

class Repack_SetUpScript : BaseScript
{
	public static void Main(string[] args)
	{
		new Repack_SetUpScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            var assemblyFile = "bin/Release/csAnt-SetUp.exe";

            var dependencies = new string[]{
                "lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll",
                "lib/HtmlAgilityPack/Net40/HtmlAgilityPack.dll",
                "bin/Release/SoftwareMonkeys.csAnt.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Contracts.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.dll",
                "bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Imports.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Processes.dll",
                "bin/Release/SoftwareMonkeys.csAnt.External.Nuget.dll",
                "bin/Release/SoftwareMonkeys.csAnt.SourceControl.Git.dll",
                "bin/Release/SoftwareMonkeys.csAnt.SetUp.dll",
                "bin/Release/SoftwareMonkeys.csAnt.Versions.dll"
            };

            new Repacker(assemblyFile, dependencies).Repack();

            return !IsError;
	}
}
