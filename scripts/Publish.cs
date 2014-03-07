//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class PublishScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PublishScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Publishing files...");
		Console.WriteLine("");
	
		ExecuteScript("PublishSetupFiles");
		ExecuteScript("PublishReleaseZips");

		return !IsError;
	}
}
