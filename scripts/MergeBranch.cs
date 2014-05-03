//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.dll;

using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Launch;
using SoftwareMonkeys.csAnt.Projects;

class MergeBranch : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new MergeBranch().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var branch = args[0];

        var commit = Arguments.ContainsAny("c", "commit");

		Console.WriteLine("");
		Console.WriteLine("Merging '" + branch + "' branch into current...");
		Console.WriteLine("");

        // Revert all assembly info files before stash
        foreach (var f in GetExcludedAssemblyInfoFiles())
        {
            RevertFile(f);
        }

        Git.Git("stash");

        Git.Git("merge --no-commit " + branch);

        foreach (var f in GetExcludedFiles())
        {
            RevertFile(f);
        }

        if (commit)
            Git.Git("commit -m \"Merged '" + branch + "' branch.\"");
        
        Git.Git("stash pop");

		return !IsError;
	}

    public void RevertFile(string nodeFile)
    {
        Git.Git("reset HEAD " + ToRelative(nodeFile));
        Git.Git("checkout -- " + ToRelative(nodeFile));
    }

    public string[] GetExcludedFiles()
    {
        var list = new List<string>();

        list.AddRange(GetExcludedNodeFiles());
        list.AddRange(GetExcludedAssemblyInfoFiles());

        return list.ToArray();
    }

    public string[] GetExcludedNodeFiles()
    {
        var list = new List<string>();

        list.AddRange(FindFiles("**.node"));

        return list.ToArray();
    }

    public string[] GetExcludedAssemblyInfoFiles()
    {
        var list = new List<string>();

        list.AddRange(FindFiles("src", "**.AssemblyInfo.cs"));

        return list.ToArray();
    }
        
}
