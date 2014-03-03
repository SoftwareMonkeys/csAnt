//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using System.Collections.Generic;
using System.IO;

class ProfileScript : BaseScript
{
	public static void Main(string[] args)
	{
		new ProfileScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		if (args.Length < 1)
			Console.WriteLine("At least one argument is expected; script name.");
		else
		{
			string scriptName = args[0];

			var profilingDir = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "profiling";

			var profilingSummaryFile = profilingDir
				+ Path.DirectorySeparatorChar
				+ "summary.txt";

			Console.WriteLine("");
			Console.WriteLine("Profiling '" + scriptName + "' script...");
			Console.WriteLine("");
			Console.WriteLine("Profiling directory:");
			Console.WriteLine(profilingDir);
			Console.WriteLine("");
			Console.WriteLine("Profiling summary:");
			Console.WriteLine(profilingSummaryFile);
			Console.WriteLine("");

			EnsureDirectoryExists(profilingDir);

			var otherArgs = new List<string>(args);

			otherArgs.RemoveAt(0);

			var otherArgsString = String.Join("", otherArgs.ToArray());

			var dataFile = "output.mlpd";

			if (File.Exists(ToAbsolute(dataFile)))
				File.Delete(ToAbsolute(dataFile));

			// Run script with profiling command
			var cmd1 = String.Format(
				"mono --profile=log {0} {1} {2}",
				"lib/csAnt/bin/Release/csAnt.exe", // TODO: Move this to a common variable/property
				scriptName,
				otherArgsString
			);

			// Generate profiling.txt file
			var cmd2 = String.Format(
				"mprof-report --out={0} {1}",
				ToRelative(profilingSummaryFile),
				dataFile
			);

			StartProcess(cmd1);

			StartProcess(cmd2);
		}

		return !IsError;
	}
}
