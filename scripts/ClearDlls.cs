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

                                DeleteDllDir(dir);
                                        
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
				DeleteDllDir(binDir);

			var objDir = dir
				+ Path.DirectorySeparatorChar
				+ "obj";

			Console.WriteLine(objDir);
	
			if (Directory.Exists(objDir))
				DeleteDllDir(objDir);
		}
	}
        
        public void DeleteDllDir(string dir)
        {
                foreach (var file in Directory.GetFiles(dir))
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
                catch (Exception ex)
                {
                        Console.WriteLine("Can't delete: " + file);
                }
        }
}
