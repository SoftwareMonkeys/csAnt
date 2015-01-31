//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SourceControl.Git.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SourceControl.Git;

class Test_MergeBranch : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_MergeBranch().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var branch = new GitBranchIdentifier().Identify();

        var gitter = new Gitter();

        gitter.Clone(OriginalDirectory, CurrentDirectory);      

        var testBranchName = "TestBranch";

        // Create a new branch and check it out
        gitter.Branch(testBranchName, true);

        var testFileName = Path.GetFullPath("test.txt");

        File.WriteAllText(testFileName, "Test content");

        gitter.Add(testFileName);

        gitter.Commit("Added test file");

        // Increment the version so the .node files conflict
        ExecuteScript("IncrementVersion", "3");

        // Commit the version related files
        ExecuteScript("CommitVersion");

        // Checkout the original branch
        gitter.Checkout(branch);

        // Run the MergeBranch script
        ExecuteScript("MergeBranch", testBranchName);

        // Run the HelloWorld script to ensure the scripting is still functional
        ExecuteScript("HelloWorld");

		return !IsError;
	}
}
