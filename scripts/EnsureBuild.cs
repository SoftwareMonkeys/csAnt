using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt;
using System.Linq;

class EnsureBuildScript : BaseScript
{
	public static void Main(string[] args)
	{
		new EnsureBuildScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var buildMode = "Release";
        if (Arguments.Contains("mode"))
            buildMode = Arguments["mode"];

        new SolutionBuildEnsurer().EnsureBuilt(CurrentDirectory, buildMode);

		return !IsError;
	}
}
