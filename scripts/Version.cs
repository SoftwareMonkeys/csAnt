//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Versions.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;

class VersionScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new VersionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	    var version = CurrentNode.Properties["Version"];
	    
	    Console.WriteLine("Version: " + version);
	    
	    return true;
	}
}
