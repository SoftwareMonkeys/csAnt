using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
	public class csAntInstaller
	{
		public string InstallPath { get; set; }
		public bool IsVerbose { get; set; }
        public bool OverwriteFiles { get;set; }

		public csAntInstaller (
			string installPath,
            bool overwriteFiles,
			bool isVerbose
		)
		{
			InstallPath = installPath;
			IsVerbose = isVerbose;
            OverwriteFiles = overwriteFiles;
		}

		public void Install (string name)
        {
            var localListFile = GetLocalInstallListFilePath (name);

            if (!String.IsNullOrEmpty (localListFile)
                && File.Exists (localListFile)) {
                
                Console.WriteLine ("List file found locally:");
                Console.WriteLine (localListFile);
                Console.WriteLine ("");

                InstallFromListFile (localListFile);
            } else {
                
                Console.WriteLine ("List file not found locally.");
                Console.WriteLine ("");

                var listUrl = "https://csant.googlecode.com/git/install/" + name + ".txt";

                InstallFromListFileUrl (name, listUrl);
            }
		}

        public string GetLocalInstallListFilePath (string name)
        {
            
            var localCsAntDir = GetLocalCsAntDir ();

            var listFile = String.Empty;

            if (!String.IsNullOrEmpty (localCsAntDir)) {
                listFile = localCsAntDir
                    + Path.DirectorySeparatorChar
                    + "install"
                    + Path.DirectorySeparatorChar
                    + name
                        + ".txt";
            }

            return listFile;
        }

        public void InstallFromListFile (string listFilePath)
        {

            if (Path.GetFullPath (listFilePath) != listFilePath)
                listFilePath = Path.GetFullPath (listFilePath);
            
                Console.WriteLine ("Installing from list file:");
                Console.WriteLine (listFilePath);
                Console.WriteLine ("");

            var lines = File.ReadAllLines (listFilePath);

            var localCsAntDir = GetLocalCsAntDir();

            foreach (var line in lines) {
                if (!String.IsNullOrEmpty(line.Trim ()))
                {
                    var parts = line.Split(',');

                    if (parts.Length == 2)
                    {
                        var file = parts[0].Trim ();

                        var toFile = InstallPath
                            + Path.DirectorySeparatorChar
                                + file.Trim (Path.DirectorySeparatorChar);
                        
                        var localFromFile = toFile.Replace(InstallPath, localCsAntDir);

                        if (!String.IsNullOrEmpty(localCsAntDir)
                            && Directory.Exists(localCsAntDir)
                            && File.Exists(localFromFile))
                        {
                            if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                                Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                            if (OverwriteFiles || !File.Exists(toFile))
                                File.Copy(localFromFile, toFile, OverwriteFiles);
                        }
                        else
                        {
                            var url = parts[1].Trim();

                            if (!File.Exists(toFile)
                                || OverwriteFiles)
                                Utilities.Download(url, toFile);
                        }
                    }
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Installation complete!");
            Console.WriteLine ("");
        }
        
        public void InstallFromListFileUrl (string name, string listFileUrl)
        {
            Console.WriteLine ("Installing from list file URL:");
            Console.WriteLine (listFileUrl);
            Console.WriteLine ("");

            var internalFilePath = InstallPath
                + Path.DirectorySeparatorChar
                + "install"
                + Path.DirectorySeparatorChar
                + name
                + ".txt";

            if (!File.Exists(internalFilePath)
                || OverwriteFiles)
                Utilities.Download(listFileUrl, internalFilePath);

            InstallFromListFile(internalFilePath);
        }

        public string GetLocalCsAntDir ()
        {
            var csAntDir = Path.GetDirectoryName (Environment.CurrentDirectory)
                + Path.DirectorySeparatorChar
                + "csAnt";
            
            var csAntDir1 = csAntDir;
            var csAntDir2 = csAntDir;

            while (true) {
                csAntDir1 = Path.GetDirectoryName (Path.GetDirectoryName (csAntDir1))
                    + Path.DirectorySeparatorChar
                    + "csAnt";

                csAntDir2 = Path.GetDirectoryName (Path.GetDirectoryName (Path.GetDirectoryName (csAntDir2)))
                    + Path.DirectorySeparatorChar
                    + "SoftwareMonkeys"
                    + Path.DirectorySeparatorChar
                    + "csAnt";

                // If all that's left of the path is "csAnt" then the folder hasn't been found
                if (csAntDir1.Trim (Path.DirectorySeparatorChar) == "csAnt")
                {
                    csAntDir = String.Empty;
                    break;
                }
                else
                {
                    if (Directory.Exists (csAntDir1)) {
                        csAntDir = csAntDir1;
                        break;
                    }

                    if (Directory.Exists (csAntDir2)) {
                        csAntDir = csAntDir2;
                        break;
                    }
                }
            }

            return csAntDir;
        }
	}
}

