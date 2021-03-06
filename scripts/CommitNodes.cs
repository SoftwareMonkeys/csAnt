using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CommitNodesScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CommitNodesScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var version = CurrentNode.Properties["Version"];

        var message = @"""Updated version: " + version + @"""";

        var patterns = new string[]{
            "*.node",
            "src/**.node"       
        };

        Git.Git(
            "reset"
        );

        foreach (var file in FindFiles(patterns))
        {
            Git.Git(
                "add",
                ToRelative(file)
            );
        }

        Git.Git(
            "commit",
            "*.node",
            "-m " + message
        );

        Git.Git(
            "push",
            "origin",
            "master"
        );
    
        return !IsError;
	}
}
