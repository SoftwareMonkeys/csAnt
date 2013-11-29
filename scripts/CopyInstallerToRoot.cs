//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;

using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CopyInstallerToRootScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CopyInstallerToRootScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		string binDirectory = ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "bin";
	
	        Console.WriteLine("");
	        Console.WriteLine("Bin directory:");
	        Console.WriteLine(binDirectory);
	        Console.WriteLine("");

                var filePath = binDirectory
			+ Path.DirectorySeparatorChar
			+ "Release"
			+ Path.DirectorySeparatorChar
			+ "csAnt-Install.exe";

		var newFilePath = CurrentDirectory
			+ Path.DirectorySeparatorChar
			+ Path.GetFileName(filePath);

		Console.WriteLine("Copying:");
		Console.WriteLine(filePath);
		Console.WriteLine("To:");
		Console.WriteLine(newFilePath);

		File.Copy(filePath, newFilePath, true);

		AddSummary("Moved csAnt-Install.exe to root.");

		return true;
	}
}
