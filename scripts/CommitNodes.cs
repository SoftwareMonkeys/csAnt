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

        Git(
            "reset"
        );

        foreach (var file in FindFiles(patterns))
        {
            Git(
                "add",
                ToRelative(file)
            );
        }

        Git(
            "commit",
            "*.node",
            "-m " + message
        );
    
        return !IsError;
	}
}
