//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;

class Test_ErrorScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_ErrorScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Error("Intentional error.");

		return !IsError;
	}
}
