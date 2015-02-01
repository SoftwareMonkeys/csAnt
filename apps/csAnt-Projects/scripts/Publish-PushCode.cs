using System;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class Publish_PushCode : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new Publish_PushCode().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        ExecuteScript("PushCode");

		return !IsError;
	}
}
