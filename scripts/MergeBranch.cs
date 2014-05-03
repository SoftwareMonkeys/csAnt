//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
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

        Git.Git("stash");

        Git.Git("merge --no-commit " + branch);

        foreach (var nodeFile in FindFiles("**.node"))
        {
            RevertFile(nodeFile);
        }

        foreach (var assemblyInfoFile in FindFiles("src", "**.AssemblyInfo.cs"))
        {
            RevertFile(assemblyInfoFile);
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
}
