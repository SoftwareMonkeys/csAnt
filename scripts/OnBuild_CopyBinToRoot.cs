//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll

using System;
using System.IO;
using SoftwareMonkeys.csAnt;

class OnBuild_CopyBinToRootScript : BaseScript
{
	public static void Main(string[] args)
	{
		new OnBuild_CopyBinToRootScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		ExecuteScript("CopyBinToRoot");

		return !IsError;
	}
}
