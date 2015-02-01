using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Linq;

class PushCodeScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new PushCodeScript().Start(args);
	}
	
	public override bool Run(string[] args)
    {
        var remotes = new string[]{};
        if (CurrentNode.Properties.ContainsKey("GitRemotes"))
            remotes = CurrentNode.Properties["GitRemotes"].Split(',');

        var branch = "master";
        if (CurrentNode.Properties.ContainsKey("Branch"))
            branch = CurrentNode.Properties["Branch"];

        foreach (var remote in remotes)
        {
            if (!String.IsNullOrEmpty(branch))
                Git.Push(remote, branch);
            else
                Git.Push(remote);
        }

		return true;
	}

}
