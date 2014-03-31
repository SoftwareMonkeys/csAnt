using System;
using System.IO;
using System.Net;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class Publish_PushToCodePlexScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new Publish_PushToCodePlexScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        GitPush("CodePlex");

		return !IsError;
	}
}
