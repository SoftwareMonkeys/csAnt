//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.IO.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.Versions.dll";
//css_ref "nunit.framework.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Versions;
using NUnit.Framework;

class Test_EnsurePackage : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_EnsurePackage().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        new FilesGrabber(
            OriginalDirectory,
            CurrentDirectory
        ).GrabOriginalFiles();
        
        var versionBefore = new VersionManager().GetVersion(CurrentDirectory);

        ExecuteScript("EnsurePackage");

        var versionAfter = new VersionManager().GetVersion(CurrentDirectory);

        Assert.AreEqual(versionBefore.ToString(), versionAfter.ToString(), "Version was incremented when it shouldn't be.");
        
		return !IsError;
	}
}
