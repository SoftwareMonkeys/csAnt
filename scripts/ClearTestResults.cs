using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ClearTestResultsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ClearTestResultsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        
		Console.WriteLine("");
		Console.WriteLine("Clearing test results...");
		Console.WriteLine("");

                var dir = CurrentDirectory
                        + Path.DirectorySeparatorChar
                        + "tests"
                        + Path.DirectorySeparatorChar
                        + "results";
                        
                Console.WriteLine("Results dir:");
                Console.WriteLine(dir);
                Console.WriteLine("");
                        
                if (Directory.Exists(dir))
                        Directory.Delete(dir, true);
                else
                {
                        Console.WriteLine("Directory not found. Skipping deletion.");
                        
                        Console.WriteLine("");
                }

		return !IsError;
	}

	public void CleanBinDirectory()
	{
		var binDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "bin";

		if (Directory.Exists(binDir))
		{
			foreach (var dir in Directory.GetDirectories(binDir))
			{
				Console.WriteLine(dir);

				Directory.Delete(dir, true);
			}
		}
	}

	public void CleanProjectDirectories()
	{
		var srcDir = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ "src";

		foreach (var dir in Directory.GetDirectories(srcDir))
		{
			var binDir = dir
				+ Path.DirectorySeparatorChar
				+ "bin";

			Console.WriteLine(binDir);

			if (Directory.Exists(binDir))
				Directory.Delete(binDir, true);

			var objDir = dir
				+ Path.DirectorySeparatorChar
				+ "obj";

			Console.WriteLine(objDir);
	
			if (Directory.Exists(binDir))
				Directory.Delete(objDir, true);
		}
	}
}
