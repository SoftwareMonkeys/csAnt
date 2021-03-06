//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.Projects.dll

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class CopyBinToLibScript : BaseProjectScript
{
    public string TemporaryDir = String.Empty;

	public static void Main(string[] args)
	{
		new CopyBinToLibScript().Start(args);
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

		int i = 0;
        int s = 0;

        TemporaryDir = GetTmpDir();

        foreach (string dir in Directory.GetDirectories(binDirectory))
        {
            var mode = Path.GetFileName(dir);
        
            foreach (string file in Directory.GetFiles(dir))
            {
	            string toFile = GetLibDir()
		            + Path.DirectorySeparatorChar
		            + mode
		            + Path.DirectorySeparatorChar
		            + "net-40" // TODO: Make this customizable via an argument
		            + Path.DirectorySeparatorChar
		            + Path.GetFileName(file);

	            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
		            Directory.CreateDirectory(Path.GetDirectoryName(toFile));

	            Console.WriteLine("Copying: "
		            + file.Replace(ProjectDirectory, "")
	            );

	            Console.WriteLine("To: "
		            + toFile.Replace(ProjectDirectory, "")
	            );

		        try
		        {

                    var timeStamp = File.GetLastWriteTime(file);

// TODO: Remove if not needed
                    // If the to file exists then move it to a temporary directory so it can keep running while the new assembly is copied into place
                    // This should prevent errors with overwriting a running executable
                    if (File.Exists(toFile))
                    {                            
                        var tmpFile = toFile.Replace(
                            CurrentDirectory,
                            TemporaryDir
                        );

                        EnsureDirectoryExists(Path.GetDirectoryName(tmpFile));

                        File.Move(toFile, tmpFile);
                    }
                       
                    var isNewer = !File.Exists(toFile)
                        || File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile);

                    if (!File.Exists(toFile) || isNewer)
                    {
        		        File.Copy(file, toFile, true);
                        File.SetLastWriteTime(toFile, timeStamp);
        		        i++;
                    }
                    else
                        s++; // TODO: Because the files are moved above they're never skipped. See if there's a way to resolve this
		        }
		        catch (Exception)
		        {
			        Console.WriteLine("Can't copy: " + file);
		        }
	        }
		}

		Console.WriteLine(i + " files copied.");
		Console.WriteLine(s + " files skipped.");

		AddSummary("Moved " + i + " files from '/bin/' to '/lib/" + ProjectName + "'");

        UpdateVersion();

		return true;
	}

    public void UpdateVersion()
    {
        if (CurrentNode.Nodes.ContainsKey("Libraries")
            && CurrentNode.Nodes["Libraries"].Nodes.ContainsKey("csAnt"))
        {
            var csAntLibNode = CurrentNode.Nodes["Libraries"].Nodes["csAnt"];

            var version = CurrentNode.Properties["Version"];

            csAntLibNode.Properties["Version"] = version;
            csAntLibNode.Save();
        }
    }

	public string GetLibDir()
	{
		return ProjectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ ProjectName
			+ Path.DirectorySeparatorChar
			+ "bin";
	}

    public override void Dispose()
    {
        base.Dispose();

        Directory.Delete(TemporaryDir, true);

    }
}
