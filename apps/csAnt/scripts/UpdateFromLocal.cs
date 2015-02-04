//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class UpdateFromLocal : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new UpdateFromLocal().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Updating...");
        Console.WriteLine("");

        // Path used during testing
        var csAntDir1 = Path.GetFullPath(
            Path.Combine(CurrentDirectory, "../../../../SoftwareMonkeys/csAnt")
        );

        // Path used during development
        var csAntDir2 = Path.GetFullPath(
            Path.Combine(CurrentDirectory, "../../SoftwareMonkeys/csAnt")
        );

        if (Directory.Exists(csAntDir1))
            UpdateFrom(csAntDir1, args);
        else if (Directory.Exists(csAntDir2))
            UpdateFrom(csAntDir2, args);
        else
            Console.WriteLine("Can't find csAnt directory.");

        Console.WriteLine("");
        Console.WriteLine("Update complete!");
        Console.WriteLine("");
		return !IsError;
	}

    public void UpdateFrom(string sourceDir, string[] args)
    {
        var allArgs = new List<string>();
        allArgs.Add(sourceDir);
        allArgs.AddRange(args);        

        StartDotNetExe("csAnt-SetUpFromLocal.exe", allArgs.ToArray());
    }
}
