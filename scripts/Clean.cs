//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CleanScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CleanScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Cleaning project...");
		Console.WriteLine("(removing directories and files)");
		Console.WriteLine("");

		CleanBinDirectory();

		CleanProjectDirectories();
		
		return true;
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
