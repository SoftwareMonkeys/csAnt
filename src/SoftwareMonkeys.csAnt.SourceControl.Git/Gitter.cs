using System;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Processes;
using System.IO;


namespace SoftwareMonkeys.csAnt.SourceControl.Git
{
    public class Gitter
    {
        public DirectoryMover Mover { get;set; }

        public ProcessStarter Starter { get;set; }

        public Gitter ()
        {
            Mover = new DirectoryMover();
            Starter = new ProcessStarter();
        }

        public void Git(params string[] arguments)
        {
            // TODO: Make this configurable and ensure it works on windows
            var gitExe = "git";

            Starter.Start(
                gitExe,
                arguments
            );
        }
        
        public void GitIn(string workingDirectory, params string[] arguments)
        {
            var originalDir = Environment.CurrentDirectory;

            Environment.CurrentDirectory = workingDirectory;

            Git(arguments);

            Environment.CurrentDirectory = originalDir;
        }
        
        public void Add(string file)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            Git ("add", file);
        }
        
        public void AddTo(string path, string file)
        {
            
            Console.WriteLine ("");
            Console.WriteLine ("Adding file to git:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            GitIn (path, "add", file);
        }

        public void AddRemote(string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + name);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            Git ("remote add " + name + " \"" + path + "\"");
        }
        
        public void AddRemoteTo(string directory, string name, string path)
        {
            Console.WriteLine("");
            Console.WriteLine("Adding git remote...");
            Console.WriteLine("Current directory:" + directory);
            Console.WriteLine("Name:" + name);
            Console.WriteLine("Path:" + path);
            Console.WriteLine("");

            GitIn (directory, "remote add " + name + " \"" + path + "\"");
        }

        public void Clone(
            string sourceDir
        )
        {
            Clone(
                sourceDir,
                Environment.CurrentDirectory
            );
        }

        public void Clone(
            string sourceDir,
            string destinationDir
        )
        {
            Console.WriteLine("");
            Console.WriteLine ("Cloning...");
            Console.WriteLine ("Source: " + sourceDir);
            Console.WriteLine ("Destination: " + destinationDir);

            var tmpDir = destinationDir + Path.DirectorySeparatorChar + "_clone";

            Git (
                "clone",
                sourceDir,
                tmpDir,
                "--verbose"
            );

            Mover.Move(tmpDir, destinationDir, true);

            Console.WriteLine("");
            Console.WriteLine("Complete");
            Console.WriteLine("");

        }
        
        public void Commit ()
        {
            Commit ("Committing added/changed files");
        }
        
        public void Commit (string message)
        {
            Console.WriteLine ("Committing added/changed files...");

            Git ("commit", "-a", "-m:'" + message + "'");
        }
        
        public void CommitTo (string directory)
        {
            CommitTo ("");
        }
        
        public void CommitTo (string directory, string message)
        {
            Console.WriteLine ("Committing added/changed files...");

            GitIn (directory, "commit", "-a", "-m:'" + message + "'");
        }

        public void Init()
        {
            Console.WriteLine ("Initializing repository:");
            Console.WriteLine ("Path: " + Environment.CurrentDirectory);

            Git ("init");
        }
        
        public void Init(string path)
        {
            Console.WriteLine ("Initializing repository:");
            Console.WriteLine ("Path: " + path);

            Environment.CurrentDirectory = path;

            Git ("init");
        }

        public void Pull(string remote)
        {
            Git ("pull", remote);
        }

        public void Pull()
        {
            Git ("pull", "-all");
        }

        public void PullTo(string directory, string remote)
        {
            GitIn (directory, "pull", remote, "master"); // Should branch be left out?
        }

        public void Push(string remote)
        {
            Git ("push", remote);
        }

        public void Push(string remote, string branch)
        {
            Git ("push", remote, branch);
        }
        
        public void PushFrom(string directory, string remote)
        {
            var originalDirectory = Environment.CurrentDirectory;

            Environment.CurrentDirectory = directory;

            Push(remote);

            Environment.CurrentDirectory = originalDirectory;
        }
        
        public void PushFromDirectoryToDirectory (string directory, string destination)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Pushing git from:");
            Console.WriteLine (directory);
            Console.WriteLine ("To:");
            Console.WriteLine (destination);
            Console.WriteLine ("");

            GitIn (directory, "push", destination);
        }

    }
}

