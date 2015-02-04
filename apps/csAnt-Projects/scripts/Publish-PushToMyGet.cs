using System;
using System.IO;
using System.Net;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using System.Collections.Generic;

class Publish_PushToMyGet : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new Publish_PushToMyGet().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var packageName = ""; // Empty means all
        if (Arguments.KeylessArguments.Length > 0)
            packageName = Arguments.KeylessArguments[0];

		ExecuteScript("PushToMyGet", packageName);

		return !IsError;
	}
}
