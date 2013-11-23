//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class BuildAllSolutionsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new BuildAllSolutionsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var arguments = new Arguments(args);

		var mode = "Release";

		if (arguments.Contains("mode"))
			mode = arguments["mode"];

		BuildAllSolutions("src", mode);

		return !IsError;
	}
}
