using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CommitAssemblyInfoFilesScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CommitAssemblyInfoFilesScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var version = CurrentNode.Properties["Version"];
        
        var status = CurrentNode.Properties["Status"];
        
        if (String.IsNullOrEmpty(status))
            status = "stable";

        var message = @"""Set version to: " + version + @" (" + status + @")""";

        var patterns = new string[]{
            "*.node",
            "src/**.node",
            "**AssemblyInfo.cs"       
        };

        Git.Git(
            "reset",
            "HEAD -- ."
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
            "-m " + message
        );
    
        return !IsError;
	}
}
