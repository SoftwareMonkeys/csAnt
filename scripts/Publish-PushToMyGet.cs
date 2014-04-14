using System;
using System.IO;
using System.Net;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class Publish_PushToMyGetScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new Publish_PushToMyGetScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var packageName = "";
        if (args.Length > 0)
            packageName = args[0];

		ExecuteScript("PushToMyGet", packageName);

		return !IsError;
	}
}
