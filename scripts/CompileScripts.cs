//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CompileScriptsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CompileScriptsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var arguments = new Arguments(args);

		CompileScripts(arguments.Contains("f"));	

		return !IsError;
	}
}
