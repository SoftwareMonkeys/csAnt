//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Versions.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;

class SetVersionScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SetVersionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
  		var versionString = args[0];

        var commit = Arguments.ContainsAny("c", "commit");

		Version version = null;

		try
		{
			version = new Version(versionString);
		}
		catch (Exception)
		{
			Error("Invalid version string '" + versionString + "'. Should be in this format: '1.2.3.4'.");
		}

		SetVersion(version);

        if (commit)
            ExecuteScript("CommitVersion");

		return !IsError;
	}
}
