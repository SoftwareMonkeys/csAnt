//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.SetUp.Install;

class InstallToScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new InstallToScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	    var packageName = "csAnt";
	    var destination = "";

        if (args.Length >= 2)
        {
            packageName = args[0];
            destination = args[1];
        }
        else if (args.Length >= 1)
        {
    	    destination = Path.GetFullPath(args[1]);
	    }

        if (destination == "")
            throw new Exception("A destination must be specified as an argument.");

        var installer = new LocalInstaller(
            CurrentDirectory,
            destination,
            packageName,
            true
        );
        installer.Install();
	    
	    var arguments = new Arguments(args);
	    
	    if (arguments.Contains("r"))
	        Relocate(destination);
	    
	    return !IsError;
	}
}
