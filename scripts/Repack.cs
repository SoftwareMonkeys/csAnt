using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class RepackScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RepackScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		//ExecuteScript("EnsureBuild");

                foreach (var script in FindScripts("Repack-*.cs"))
                {
                    Console.WriteLine(script);

                    ExecuteScript(script);
                }

		return !IsError;
	}
}
