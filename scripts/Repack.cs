using System;
using System.IO;
using SoftwareMonkeys.csAnt;

class RepackScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RepackScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        Console.WriteLine("");
        Console.WriteLine("Repacking assemblies...");
        Console.WriteLine("");

        var arguments = new Arguments(args);

        var buildMode = "Release";
        if (arguments.Contains("mode"))
            buildMode = arguments["mode"];

        Console.WriteLine("Build mode:");
        Console.WriteLine(buildMode);
        Console.WriteLine("");

        foreach (var script in FindScripts("Repack-*.cs"))
        {
            Console.WriteLine(script);

            ExecuteScript(script, "-mode=" + buildMode);
        }

		return !IsError;
	}
}
