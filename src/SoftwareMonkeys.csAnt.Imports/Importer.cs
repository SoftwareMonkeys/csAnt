using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SourceControl.Git;


namespace SoftwareMonkeys.csAnt.Imports
{
    public class Importer
    {
        private string workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                if (!String.IsNullOrEmpty(workingDirectory))
                    return workingDirectory;
                else
                    return Environment.CurrentDirectory;
            }
            set { workingDirectory = value; }
        }

        private string stagingDirectory;
        public string StagingDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(stagingDirectory))
                    stagingDirectory = GetStagingDirectory();
                return stagingDirectory;
            }
            set
            {
                stagingDirectory = value;
            }
        }
        public bool IsVerbose { get;set; }
        public Gitter Git { get;set; }
        public IFileFinder Finder { get;set; }
        public FileSync FileSync { get;set; }

        public Importer ()
        {
            StagingDirectory = GetStagingDirectory();
            Git = new Gitter();
            Finder = new FileFinder();
            FileSync = new FileSync();
        }

        public string AddImport (string importProject, string importProjectPath)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding import...");
            Console.WriteLine("");
            Console.WriteLine("Project: " + importProject);
            Console.WriteLine("Path:");
            Console.WriteLine(importProjectPath);
            Console.WriteLine("");

            if (String.IsNullOrEmpty(StagingDirectory))
                throw new Exception("StagingDirectory is not set."); // TODO: Create custom error class

            var importProjectName = importProject;

            var sourceDirectory = importProjectPath;

            if (!sourceDirectory.Contains("http")
                && !sourceDirectory.Contains (WorkingDirectory))
                sourceDirectory = Path.GetFullPath (importProjectPath);

            var currentDirectory = WorkingDirectory;

            var parentDirectory = Path.GetDirectoryName (currentDirectory);

            // Create the path to the directory containing the local copy of the import
            var importStagingDirectory = StagingDirectory
                + Path.DirectorySeparatorChar
                + importProjectName;

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Adding Git import...");
                Console.WriteLine ("");

                Console.WriteLine ("Import project: " + importProjectName);

                Console.WriteLine ("Source path: " + sourceDirectory);

                Console.WriteLine ("Current directory: " + currentDirectory);

                Console.WriteLine ("Parent directory: " + parentDirectory);

                Console.WriteLine ("Import directory: " + importStagingDirectory);

                Console.WriteLine ("");
            }

            Directory.CreateDirectory(importStagingDirectory);

            Git.Clone(sourceDirectory, importStagingDirectory);

            var sourceFile = importStagingDirectory
                + Path.DirectorySeparatorChar
                    + "source.txt";

            File.WriteAllText(sourceFile, sourceDirectory);

            return importStagingDirectory;
        }

        public void AddImportPattern(string projectName, string pattern)
        {
            var listPath = StagingDirectory
                + Path.DirectorySeparatorChar
                + projectName
                + Path.DirectorySeparatorChar
                + "patterns.txt";

            var patterns = new List<string>();

            pattern = pattern.Trim('/').Trim ('\\');
            
            if (File.Exists (listPath))
                patterns.AddRange (File.ReadAllLines(listPath));

            if (!patterns.Contains(pattern))
                patterns.Add (pattern);

            DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(listPath));

            File.WriteAllLines(listPath, patterns.ToArray());
        }
        public void ImportFile (string projectName, string relativePath)
        {
            ImportFile(projectName, relativePath, "/", false);
        }

        public void ImportFile (string projectName, string relativePath, string destination, bool flattenHeirarchy)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Importing files...");
            Console.WriteLine ("Project name: " + projectName);
            Console.WriteLine ("Path:");
            Console.WriteLine (relativePath);
            if (IsVerbose) {
                Console.WriteLine ("Destination:");
                Console.WriteLine (destination);
                Console.WriteLine ("Flatten path:");
                Console.WriteLine (flattenHeirarchy.ToString());
                Console.WriteLine ("");
            }
            
            AddImportPattern(projectName, relativePath);

            if (!ImportExists (projectName))
                throw new Exception ("Import project '" + projectName + "' not found."); // TODO: Make a custom exception class
            else {
                Refresh(projectName);

                var importDir = StagingDirectory
                    + Path.DirectorySeparatorChar
                        + projectName;

                if (Directory.Exists(importDir))
                {
                    foreach (var file in Finder.FindFiles(importDir, relativePath))
                    {
                        var toFile = file.Replace (importDir, WorkingDirectory);

                        // TODO: Implement flattenHeirarchy
                        //if (flattenHeirarchy)
                            //toFile = toFile.Replace (
                        
                        Console.WriteLine ("");
                        Console.WriteLine ("Copying file:");
                        Console.WriteLine (file);
                        if (IsVerbose) {
                            Console.WriteLine ("To:");
                            Console.WriteLine (toFile);
                        }
                        Console.WriteLine ("");

                        DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(toFile));

                        if (File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile))
                        {
                            File.Copy(file, toFile, true);
                            if (IsVerbose)
                                Console.WriteLine ("File is newer. Using.");
                        }
                        else if (File.GetLastWriteTime(file) == File.GetLastWriteTime(toFile))
                        {
                            if (IsVerbose)
                                Console.WriteLine ("File is same age. Skipping.");
                        }
                        else
                        {
                            if (IsVerbose)
                                Console.WriteLine ("File is older. Skipping.");
                        }
                    }
                }
                else
                    Console.WriteLine ("Import directory not found: " + importDir);
            }
        }
        
        public string GetStagingDirectory()
        {
            var parentDirectory = Path.GetDirectoryName(WorkingDirectory);

            var stagingDirectory = parentDirectory
                + Path.DirectorySeparatorChar
                + Path.GetFileName(WorkingDirectory)
                    + "-Imports";


            return stagingDirectory;
        }
        
        public bool ImportExists(string importProjectName)
        {
            var path = StagingDirectory
                + Path.DirectorySeparatorChar
                    + importProjectName;

            Console.WriteLine ("");
            Console.WriteLine ("Import exists:");
            Console.WriteLine (path);
            Console.WriteLine (Directory.Exists(path).ToString());
            Console.WriteLine ("");

            return Directory.Exists(path);
        }

        public void Refresh (string name)
        {
            var dir = StagingDirectory
                + Path.DirectorySeparatorChar
                    + name;

            Git.PullTo(dir, "origin");

        }

        public void Sync (string projectName, string projectPath)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Synchronising files between imported project...");
            Console.WriteLine ("Project:");
            Console.WriteLine (projectName);
            Console.WriteLine ("Path:");
            Console.WriteLine (projectPath);
            Console.WriteLine ("");

            var importedProjectPath = StagingDirectory
                + Path.DirectorySeparatorChar
                + projectName;

            var patternsFile = importedProjectPath
                + Path.DirectorySeparatorChar
                + "patterns.txt";

            if (!File.Exists (patternsFile)) {
                throw new Exception ("No import/export patterns have been set for '" + projectName + "' import. Add them by using the ImportFile and ExportFile functions."); // TODO: Create a custom exception class
            } else {
                var patterns = File.ReadAllLines (patternsFile);

                foreach (var pattern in patterns) {
                    Console.WriteLine ("");
                    Console.WriteLine ("Pattern:");
                    Console.WriteLine (pattern);
                    Console.WriteLine ("");

                    FileSync.Sync (WorkingDirectory, importedProjectPath, pattern);
                    Git.AddTo (importedProjectPath, pattern);
                }
                
                var sourcePath = File.ReadAllText (importedProjectPath + Path.DirectorySeparatorChar + "source.txt");
                    
                // Commit import project
                Git.CommitTo (importedProjectPath, "Sync.");

                // Get the remote name
                var remoteName = Path.GetFileName (importedProjectPath);

                // Add the importable project directory as a remote to the original
                Git.AddRemoteTo (sourcePath, remoteName, importedProjectPath);

                // Pull changes to back to the original project
                Git.PullTo (sourcePath, remoteName);
            }
        }

        public void Sync(string toDir)
        {
            Sync(WorkingDirectory, toDir);
        }
        
        public void ExportFile (string projectName, string relativePath)
        {
            ExportFile(projectName, relativePath, "/", false);
        }

        public void ExportFile(string projectName, string relativePath, string destination, bool flattenHeirarchy)
        {
            if (PathConverter.IsAbsolute(relativePath))
                relativePath = PathConverter.ToRelative(relativePath);

            destination = PathConverter.ToAbsolute(destination);

            AddImportPattern(projectName, relativePath);

            Console.WriteLine ("");
            Console.WriteLine ("Exporting files...");
            Console.WriteLine ("Project name:");
            Console.WriteLine (projectName);
            Console.WriteLine ("Path:");
            Console.WriteLine (relativePath);
            Console.WriteLine ("Destination:");
            Console.WriteLine (destination);
            Console.WriteLine ("Flatten path:");
            Console.WriteLine (flattenHeirarchy.ToString());
            Console.WriteLine ("");
            Console.WriteLine ("Files:");

            if (!ImportExists (projectName))
                throw new Exception ("Import project '" + projectName + "' not found."); // TODO: Create custom exception class
            else {
                Refresh(projectName);

                var importedProjectDirectory = StagingDirectory
                    + Path.DirectorySeparatorChar
                        + projectName;
                
                Console.WriteLine ("");
                Console.WriteLine ("Relative path:");
                Console.WriteLine (relativePath);
                Console.WriteLine ("");

                foreach (var file in Finder.FindFiles(Environment.CurrentDirectory, relativePath))
                {
                    var fixedPath = PathConverter.ToRelative(relativePath);
                    if (flattenHeirarchy)
                        fixedPath = Path.GetFileName(relativePath);

                    Console.WriteLine ("");
                    Console.WriteLine ("Fixed path:");
                    Console.WriteLine (fixedPath);
                    Console.WriteLine ("");

                    var toFile = importedProjectDirectory
                        + Path.DirectorySeparatorChar
                        + fixedPath;
                    
                    Console.WriteLine ("");
                    Console.WriteLine ("Exporting (copying) file:");
                    Console.WriteLine (file);
                    Console.WriteLine ("To:");
                    Console.WriteLine (toFile);
                    Console.WriteLine ("");

                    DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(toFile));

                    File.Copy(file, toFile);

                    var sourcePath = File.ReadAllText(importedProjectDirectory + Path.DirectorySeparatorChar + "source.txt");
                    
                    Console.WriteLine ("Source path:");
                    Console.WriteLine (sourcePath);

                    Git.AddTo(importedProjectDirectory, toFile);

                    // TODO: Find a better way to get the project name
                    var name = Path.GetFileName(WorkingDirectory);

                    Git.CommitTo (importedProjectDirectory, "Exported from '" + name + "' project.");

                    // Get the remote name
                    var remoteName = Path.GetFileName(importedProjectDirectory);

                    // Add the importable project directory as a remote to the original
                    Git.AddRemoteTo(sourcePath, remoteName, importedProjectDirectory);

                    // Pull changes to back to the original project
                    Git.PullTo(sourcePath, remoteName);
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Exporting complete.");
            Console.WriteLine ("");
        }
    }
}

