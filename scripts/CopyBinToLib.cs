//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class CopyBinToLibScript : BaseScript
{
	public static void Main(string[] args)
	{
		new CopyBinToLibScript().Start();
	}
	
	public void Start()
	{
		string binDirectory = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "bin"
			+ Path.DirectorySeparatorChar
			+ "Release";

		int i = 0;

		foreach (string file in Directory.GetFiles(binDirectory))
		{
			i++;

			string toFile = ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ ProjectName
				+ Path.DirectorySeparatorChar
				+ "bin"
				+ Path.DirectorySeparatorChar
				+ "Release"
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(file);

			Console.WriteLine("Copying: "
				+ file.Replace(ProjectDirectory, "")
			);

			Console.WriteLine("To: "
				+ toFile.Replace(ProjectDirectory, "")
			);

			File.Copy(file, toFile, true);
		}

		Console.WriteLine(i + " files copied.");
	}
}
