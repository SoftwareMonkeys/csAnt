//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.IO.Contracts.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SourceControl.Git.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.SetUp.dll;
//css_ref ../lib/NUnit.2.6.0.12051/lib/nunit.framework.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Projects.Tests.Scripting;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Launch;
using NUnit.Framework;

[TestFixture]
public class Test_CloneToScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_CloneToScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{	
        Prepare();

        var testProjectDir = Path.Combine(CurrentDirectory, "../TestProject");

        ExecuteScript(
            "CloneTo",
            testProjectDir
        );

		return !IsError;
	}

    public void Prepare()
    {        
		new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalScriptingFiles();

        Git.Clone(OriginalDirectory, CurrentDirectory);
    }
	
}
