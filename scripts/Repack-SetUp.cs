using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp;
using System.Collections.Generic;

class Repack_SetUpScript : BaseScript
{
	public static void Main(string[] args)
	{
		new Repack_SetUpScript().Start(args);
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
