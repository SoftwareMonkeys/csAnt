//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Collections.Generic;
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

        var list = new List<string>();
        list.Add("-update");
        list.Add("-info=false");
        list.AddRange(args);

        StartDotNetExe("csAnt-SetUp.exe", list.ToArray());

        Console.WriteLine("");
        Console.WriteLine("Update complete!");
        Console.WriteLine("");

		return !IsError;
	}
}
