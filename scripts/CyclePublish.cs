//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.FileNodes.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.FileNodes;
using SoftwareMonkeys.csAnt.IO;

class CyclePublishScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new CyclePublishScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Starting a full release cycle.");
		Console.WriteLine("");

		// Git clone the project to another directory
		var tmpDir = CloneToTmpDirectory();

		GrabFiles(tmpDir);

		// Move to the cloned directory
		Relocate(tmpDir);

		CreateNodes();

        IncrementVersion(3);

		// Build the cloned source code
		ExecuteScript("EnsurePackage");

        ReturnPackages();

        // Commit the file nodes containing the updated versions
        ExecuteScript("CommitVersion");

        Git.Push("origin", "master");

		if (!IsError)
		{
			// Publish files
			ExecuteScript("Publish");
		}

		return !IsError;
	}

    public void ReturnPackages()
    {
        var pkgDir = Path.Combine(CurrentDirectory, "pkg");

        foreach (var dir in Directory.GetDirectories(pkgDir))
        {
            var file = GetNewestFile(dir);
            
            var toFile = file.Replace(CurrentDirectory, OriginalDirectory);

            // TODO: Should the file be deleted and overwritten?
            if (File.Exists(toFile))
                File.Delete(toFile);

            File.Copy(file, toFile);
        }
    }

	public void GrabFiles(string tmpDir)
	{
		new FilesGrabber(
			OriginalDirectory,
			tmpDir
		).GrabOriginalFiles(
			"*.exe",
			"bin/**",
			"lib/nuget.exe",
			"lib/csAnt/**",
			"lib/cs-script/**",
			"lib/HtmlAgilityPack/**",
			"lib/ILRepack.1.25.0/**",
			"lib/FileNodes/**",
			"lib/NUnit/**",
			"lib/NUnitResults/**",
			"lib/Newtonsoft.Json.6.0.1/lib/net40/**",
			"lib/SharpZipLib/**",
			"pkg/*"
		);
	}

	public string CloneToTmpDirectory()
	{
		Console.WriteLine("Cloning to tmp directory...");

		var originalDirectory = CurrentDirectory;

		var tmpDirectory = GetTmpDir();

		Console.WriteLine("Tmp directory:");
		Console.WriteLine(tmpDirectory);

		Directory.CreateDirectory(tmpDirectory);

		Git.Clone(ProjectDirectory, tmpDirectory);

		AddSummary("Cloned project to: " + tmpDirectory);

		CopySecurityCode(originalDirectory, tmpDirectory);

		return tmpDirectory;
	}

	public void CopySecurityCode(string fromDir, string toDir)
	{
	    CopySecurityNode(fromDir, toDir);
	
		var file = fromDir
			+ Path.DirectorySeparatorChar
			+ "_security"
			+ Path.DirectorySeparatorChar
			+ "MyGet"
			+ Path.DirectorySeparatorChar
			+ "MyGet.node";

		var tofile = file.Replace(fromDir, toDir);

                Console.WriteLine("Copying security node to:");
                Console.WriteLine(tofile);
                Console.WriteLine("From:");
                Console.WriteLine(file);

		EnsureDirectoryExists(Path.GetDirectoryName(tofile));

        if (!File.Exists(tofile))
			File.Copy(file, tofile);
		
		CurrentNode.Nodes["Security"].Nodes[ProjectName] = new FileNode(
	                tofile,
	                new FileNodeSaver()
		);
	}
	
	public void CopySecurityNode(string fromDir, string toDir)
	{
		var nodeFile = fromDir
			+ Path.DirectorySeparatorChar
			+ "_security"
			+ Path.DirectorySeparatorChar
			+ "Security.node";

		var toNodeFile = nodeFile.Replace(fromDir, toDir);

        Console.WriteLine("Copying security node to:");
        Console.WriteLine(toNodeFile);
        Console.WriteLine("From:");
        Console.WriteLine(nodeFile);

		EnsureDirectoryExists(Path.GetDirectoryName(toNodeFile));

        if (!File.Exists(toNodeFile))
    		File.Copy(nodeFile, toNodeFile);
		
		CurrentNode.Nodes["Security"] = new FileNode(
	                toNodeFile,
	                new FileNodeSaver()
		);
	}
}
