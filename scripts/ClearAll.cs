//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ClearAllScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ClearAllScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            Console.WriteLine("");
            Console.WriteLine("Clearing project...");
            Console.WriteLine("");

            ExecuteScript("ClearDlls");
            ExecuteScript("ClearTmp");
            ExecuteScript("ClearLogs");
            ExecuteScript("ClearTestResults");
            ExecuteScript("ClearBak");

            return !IsError;
	}
}
