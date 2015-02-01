using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;

class CycleIncrementVersionScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CycleIncrementVersionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        ExecuteScript("IncrementVersion");

        ExecuteScript("GenerateAssemblyInfoFiles");

        ExecuteScript("CommitVersion");
         
		return !IsError;
	}
}
