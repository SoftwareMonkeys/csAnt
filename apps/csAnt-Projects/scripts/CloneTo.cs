//css_ref "SoftwareMonkeys.csAnt.SetUp.dll";

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.SetUp;
using SoftwareMonkeys.csAnt.SetUp.Deploy.Launch;
using SoftwareMonkeys.csAnt.Projects;

class CloneToScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CloneToScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Cloning to directory...");
		Console.WriteLine("");

        var destination = args[0];
        destination = ToAbsolute(destination);

        bool clear = Arguments.ContainsAny("c", "clear");
        bool setup = Arguments.ContainsAny("s", "setup");

        if (clear)
            ClearDestination(destination);

        Git.Clone(CurrentDirectory, destination);

        if (setup)
            new SetUpFromLocalScriptLauncher().Launch(CurrentDirectory, destination);

		return !IsError;
	}

    public void ClearDestination(string destination)
    {
        if (Directory.Exists(destination))
        {
            foreach (var file in Directory.GetFiles(destination))
            {
                Console.WriteLine(file.Replace(destination, ""));
                File.Delete(file);
            }

            foreach (var dir in Directory.GetDirectories(destination))
            {
                Console.WriteLine(dir.Replace(destination, ""));
                Directory.Delete(dir, true);
            }
        }
    }

}
