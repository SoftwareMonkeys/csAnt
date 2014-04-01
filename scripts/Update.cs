//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class UpdateScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new UpdateScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Updating...");
        Console.WriteLine("");

        StartProcess("csAnt-SetUp.exe", "-update");

		return !IsError;
	}
}
