using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
	public class PackageInstaller
	{
		public string WorkingDirectory { get; set; }
		public bool IsVerbose { get; set; }
        public bool OverwriteFiles { get;set; }

        #region Directory structure
        public string LibrariesFolderName = "lib";
        #endregion

        public string[] Repositories { get; set; }

        public string PackageUrlFormat = "https://csant.googlecode.com/git/install/{0}.txt";

        public PackageParser Parser { get;set; }

		public PackageInstaller (
			string workingDirectory,
            string[] repositories,
            bool overwriteFiles,
			bool isVerbose
		)
		{
			WorkingDirectory = workingDirectory;
			IsVerbose = isVerbose;
            OverwriteFiles = overwriteFiles;
            
            Repositories = repositories;

            Parser = new PackageParser();

		}

		public void Install (string name)
        {
            EnsurePrerequisites();

            var localListFile = GetLocalPackagePath (name);

            if (!String.IsNullOrEmpty (localListFile)
                && File.Exists (localListFile)) {
                
                Console.WriteLine ("Package file found locally:");
                Console.WriteLine (localListFile);
                Console.WriteLine ("");

                InstallPackageFromListFile (localListFile);
            } else {
                
                Console.WriteLine ("List file not found locally.");
                Console.WriteLine ("");

                var listUrl = GetFileListUrl(name);

                GetPackageFromListFileUrl (name, listUrl);
            }
		}

        public string GetFileListUrl(string name)
        {
            return "https://csant.googlecode.com/git/install/" + name + ".txt";
        }

        public string GetLocalPackagePath (string name)
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

        public void InstallPackageFromListFile (string listFilePath)
        {
            EnsurePrerequisites();

            if (Path.GetFullPath (listFilePath) != listFilePath)
                listFilePath = Path.GetFullPath (listFilePath);
            
            Console.WriteLine ("Installing from list file:");
            Console.WriteLine (listFilePath);
            Console.WriteLine ("");

            var lines = GetFileList (listFilePath);

            foreach (var line in lines) {
                if (!String.IsNullOrEmpty (line.Trim ())) {
                    InstallFile (line);
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Installation complete!");
            Console.WriteLine ("");
        }

        /// <summary>
        /// Installs the file according to the provided line (from a list).
        /// </summary>
        /// <param name='line'>
        /// The line containing the file name and location.
        /// </param>
        public void InstallFile (string line)
        {
            var file = Parser.GetRelativeFileFromItem (line);
            var locationPath = Parser.GetLocationPathFromItem (line);

            var toFile = GetDestinationFile (file);

            if (FoundLocally (line)) {
                CopyFromLocal (line);
            } else if (IsUrl (locationPath)) {
                DownloadUtility.Download (locationPath, toFile, OverwriteFiles);
            }

            if (ZipUtility.IsZipFile (toFile))
                Unzip (line);
        }

        public bool IsUrl (string path)
        {
            return path.ToLower().StartsWith("http");
        }
        // TODO: Remove if not needed
        /*public void GetSharpZipLib ()
        {
            var internalPath = GetSharpZipLibPath ();

            if (!File.Exists (internalPath)) {
                var onlinePath = "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qYmZkRFIyN3NMN1E";

                DownloadUtility.Download (onlinePath, internalPath);
            }
        }*/

        public string GetSharpZipLibPath ()
        {
            return WorkingDirectory
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                + "SharpZipLib"
                + Path.DirectorySeparatorChar
                + "ICSharpCode.SharpZipLib.dll";
        }

        public string GetDestinationFile(string relativeFile)
        {
            return WorkingDirectory
                    + Path.DirectorySeparatorChar
                    + relativeFile.Trim (Path.DirectorySeparatorChar);
       }

        public bool FoundLocally(string line)
        {
            var toFile = GetDestinationFile(Parser.GetRelativeFileFromItem(line));

            var localCsAntDir = GetLocalCsAntDir();

            var localFromFile = GetLocalFromFile(toFile);

            return !String.IsNullOrEmpty (localCsAntDir)
                    && Directory.Exists (localCsAntDir)
                    && File.Exists (localFromFile);
        }

        /// <summary>
        /// Copies the specified file from the local location.
        /// </summary>
        /// <param name='toFile'>
        /// The destination file.
        /// </param>
        public void CopyFromLocal (string toFile)
        {
            var localFromFile = GetLocalFromFile(toFile);

            if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                Directory.CreateDirectory (Path.GetDirectoryName (toFile));

            if (OverwriteFiles || !File.Exists (toFile))
                File.Copy (localFromFile, toFile, OverwriteFiles);
        }
        
        static public void Unzip(string line)
        {
            throw new NotImplementedException();
            // TODO: Remove if not needed
            //GetSharpZipLib();

           /* var file = Parser.GetRelativeFileFromItem(line);

            var fromFile = PackageParser.GetLocationPathFromItem(line);

            var toFile = GetDestinationFile(file);

            var unzipLocation = PackageParser.GetUnzipDestinationFromLine(line);

            var subPath = PackageParser.GetUnzipSubPathFromLine(line);

            Unzip (fromFile, unzipLocation, subPath);*/
        }

        /// <summary>
        /// Gets the path to the local copy of the file.
        /// </summary>
        /// <returns>
        /// The path to the local file.
        /// </returns>
        /// <param name='toFile'>
        /// The destination file.
        /// </param>
        public string GetLocalFromFile(string toFile)
        {
            var localCsAntDir = GetLocalCsAntDir();

            return toFile.Replace (WorkingDirectory, localCsAntDir);
        }
        
        public void GetPackageFromListFileUrl (string name, string listFileUrl)
        {
            Console.WriteLine ("Installing from list file URL:");
            Console.WriteLine (listFileUrl);
            Console.WriteLine ("");

            var internalFilePath = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "install"
                + Path.DirectorySeparatorChar
                + name
                + ".txt";

            if (!File.Exists(internalFilePath)
                || OverwriteFiles)
                DownloadUtility.Download(listFileUrl, internalFilePath, OverwriteFiles);

            InstallPackageFromListFile(internalFilePath);
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

        public string[] GetFileList(string listFilePath)
        {
            var list = new List<string>();

            var fileLines = File.ReadAllLines (listFilePath);

            for (int i = 0; i < fileLines.Length; i++)
            {
                var line = fileLines[i];

                // If the line starts with a plus then it's including another install file
                if (line.StartsWith("+"))
                {
                    var includeListFilePath = Path.GetDirectoryName(listFilePath)
                        + Path.DirectorySeparatorChar
                            + line.Trim ('+').Trim();

                    if (!includeListFilePath.EndsWith(".txt"))
                        includeListFilePath += ".txt";

                    foreach (var l in GetFileList (includeListFilePath))
                        if (!list.Contains(l))
                            list.Add (l);
                }
                else if (
                        !line.StartsWith("#") // # symbol denotes a comment
                        && !String.IsNullOrWhiteSpace(line)
                    )
                {
                    list.Add (line);
                }
            }

            return list.ToArray();
        }


        public void EnsurePrerequisites ()
        {
            // If the csAnt lib directory doesn't exist then install the "csAnt-Install" package
            if (!CsAntLibDirExists ()) {
                Install ("csAnt-Install");
            }
        }

        public string GetCsAntLibDir()
        {
            return WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "lib"
                    + Path.DirectorySeparatorChar
                    + "csAnt";
        }

        public bool CsAntLibDirExists()
        {
            return Directory.Exists(GetCsAntLibDir());
        }
	}
}

