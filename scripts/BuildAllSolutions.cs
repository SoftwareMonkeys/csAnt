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
	
	public override bool Start(string[] args)
	{
		var parser = new Arguments(args);

		var mode = "Release";

		if (parser.Contains("mode"))
			mode = parser["mode"];

		BuildAllSolutions("src", mode);

		return !IsError;
	}
}
