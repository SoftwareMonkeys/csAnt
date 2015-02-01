using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ClearDllsScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ClearDllsScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Clearing DLLs from project...");
		Console.WriteLine("");

		CleanBinDirectory();

		CleanProjectDirectories();
		
		AddSummary("Cleared the project of .dll files.");

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

                DeleteDllDir(dir, true);
                                        
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
				DeleteDllDir(binDir, true);

			var objDir = dir
				+ Path.DirectorySeparatorChar
				+ "obj";

			Console.WriteLine(objDir);
	
			if (Directory.Exists(objDir))
				DeleteDllDir(objDir, true);
		}
	}
        
        public void DeleteDllDir(string dir, bool recursive)
        {
                var searchOption = SearchOption.TopDirectoryOnly;

                if (recursive)
                    searchOption = SearchOption.AllDirectories;

                foreach (var file in Directory.GetFiles(dir, "*", searchOption))
                {
                        DeleteDll(file);
                }
        }
        
        public void DeleteDll(string file)
        {
                try
                {
                        File.Delete(file);
                }
                catch (Exception)
                {
                        Console.WriteLine("Can't delete: " + file);
                }
        }
}
