//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.FileNodes.dll;
//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.IO.dll;

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

        var packageName = ""; // Empty means all
        if (Arguments.KeylessArguments.Length > 0)
            packageName = Arguments.KeylessArguments[0];

        var version = "";
        if (Arguments.ContainsAny("version"))
            version = Arguments["version"];

		// Clone (using git) from the project to another tmp directory
		var tmpDir = CloneToTmpDirectory();

        // Grab any files required to continue
		GrabFiles(tmpDir);

		// Move to the tmp directory
		Relocate(tmpDir);

        // Create any missing file nodes (*.node files)
		CreateNodes();

        if (!String.IsNullOrEmpty(version))
        {
            ExecuteScript("SetVersion", version);
        }
        else
            ExecuteScript("DetermineVersionFromMyGet");

        Nodes.Refresh();
     
        // Increment the 3rd position of the version for each publishing cycle
        IncrementVersion(3);

		// Build and package the cloned source code (the package script will trigger build cycle if necessary)
        if (!String.IsNullOrEmpty(packageName))
    		ExecuteScript("CyclePackage", packageName, "-skipincrement");
        else
            ExecuteScript("CyclePackage", "-skipincrement");

        // Commit the file version information to source control
        ExecuteScript("CommitVersion");

        // Return the created packages back to the original project /pkg/ directory
        ReturnPackages();

        var branch = "master";
        if (CurrentNode.Properties.ContainsKey("Branch"))
            branch = CurrentNode.Properties["Branch"];

        // TODO: Make the remotes configurable
        Git.Push("origin", branch, "-f");

		if (!IsError)
		{
			// Publish files
			ExecuteScript("Publish", packageName);
		}

		return !IsError;
	}

    public void ReturnPackages()
    {
        var pkgDir = Path.Combine(CurrentDirectory, "pkg");

        foreach (var dir in Directory.GetDirectories(pkgDir))
        {
            var file = GetNewestFile(dir);
            
            if (!String.IsNullOrEmpty(file))
            {
                var toFile = file.Replace(CurrentDirectory, OriginalDirectory);

                // TODO: Should the file be deleted and overwritten?
                if (File.Exists(toFile))
                    File.Delete(toFile);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                File.Copy(file, toFile);
            }
        }
    }

	public void GrabFiles(string tmpDir)
	{
		var grabber = new FilesGrabber(
			OriginalDirectory,
			tmpDir
		);

        grabber.Grab(grabber.BinFilePatterns);
        grabber.Grab(grabber.LibFilePatterns);
        grabber.Grab(grabber.PackageSpecFilePatterns);
        grabber.Grab(grabber.TestResultsPatterns);
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
