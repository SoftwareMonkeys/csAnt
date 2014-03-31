using System;
using SoftwareMonkeys.csAnt;

class OnPackage_PushToPackageServer : BaseScript
{
	public static void Main(string[] args)
	{
		new OnPackage_PushToPackageServer().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("PushToPackageServer");

		return !IsError;
	}
}
