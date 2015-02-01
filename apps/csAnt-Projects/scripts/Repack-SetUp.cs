//css_ref "SoftwareMonkeys.csAnt.SetUp.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp.Repack;
using System.Collections.Generic;

class Repack_SetUp : BaseScript
{
	public static void Main(string[] args)
	{
		new Repack_SetUp().Start(args);
	}
	
	public override bool Run(string[] args)
	{
            var buildMode = "Release";
            if (Arguments.Contains("mode"))
                buildMode = Arguments["mode"];

            new SetUpRepacker(buildMode).Repack();

            return !IsError;
	}
}
