//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

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

        var status = CurrentNode.Properties.ContainsKey("Status")
            ? CurrentNode.Properties["Status"]
            : "";

        var list = new List<string>();
        list.Add("-update");
        list.Add("-info=false");
        list.Add("-status=" + status);
        list.AddRange(args);

        StartDotNetExe("csAnt-SetUp.exe", list.ToArray());

        Console.WriteLine("");
        Console.WriteLine("Update complete!");
        Console.WriteLine("");

		return !IsError;
	}
}
