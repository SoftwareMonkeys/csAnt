//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.dll";
//css_ref "SoftwareMonkeys.csAnt.Tests.Scripting.dll";
//css_ref "SoftwareMonkeys.csAnt.IO.dll";
//css_ref "SoftwareMonkeys.csAnt.IO.Contracts.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll";
//css_ref "nunit.framework.dll";

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
using NUnit.Framework;

[TestFixture]
public class Test_IncrementVersionScript : BaseProjectTestScript
{
	public static void Main(string[] args)
	{
		new Test_IncrementVersionScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{	
		new FilesGrabber(
			OriginalDirectory,
			CurrentDirectory
		).GrabOriginalFiles();

        Nodes.Refresh();

	    IncrementVersion();

		Assert.IsTrue(!IsError);

		return !IsError;
	}
	
}
