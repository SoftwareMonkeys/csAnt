//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class InstallToScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new InstallToScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	    var name = args[0];

	    var destination = Path.GetFullPath(args[1]);
	    
	    InstallTo(name, destination);
	    
	    var arguments = new Arguments(args);
	    
	    if (arguments.Contains("r"))
	      Relocate(destination);
	    
	    return !IsError;
	}
}
