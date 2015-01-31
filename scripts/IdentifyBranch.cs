using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.SourceControl.Git;

class IdentifyBranchScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new IdentifyBranchScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        
		Console.WriteLine("");
		Console.WriteLine("Identifying branch...");
		Console.WriteLine("");

        var nodeBranch = CurrentNode.Properties["Branch"];        

        var identifier = new GitBranchIdentifier();
        
        var branch = identifier.Identify();
        
        Console.WriteLine("Branch: " + branch);
        
        if (nodeBranch != branch)
        {
            CurrentNode.Properties["Branch"] = branch;
            CurrentNode.Save();
            
            Console.WriteLine("Updated branch in file node properties.");
        }
        else
        {
            Console.WriteLine("The branch name specified in the file node properties matches the current branch. Keeping as is.");
        }

		return !IsError;
	}

}
