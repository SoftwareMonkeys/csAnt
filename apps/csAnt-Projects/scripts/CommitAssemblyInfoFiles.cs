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

        var message = @"""Updated version: " + version + @"""";

        var patterns = new string[]{
            "src/**/AssemblyInfo.cs"       
        };

        Git.Reset();

        foreach (var file in FindFiles(patterns))
        {
            Git.Add(
                ToRelative(file)
            );
        }

        Git.Git(
            "commit",
            "*.node",
            "-m " + message
        );

        Git.Push(
            "origin",
            "master"
        );
    
        return !IsError;
	}
}
