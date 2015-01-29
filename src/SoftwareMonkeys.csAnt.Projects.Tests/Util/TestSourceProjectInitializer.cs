using System;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.SourceControl.Git;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Util
{
    public class TestSourceProjectInitializer
    {
        public string OriginalDirectory { get; set; }
        public string TestSourceDirectory { get; set; }
        public string Branch { get; set; }
        public IFileFinder FileFinder;
        public ScriptExecutor Executor;

        public bool GrabOriginalFiles = true;
        public bool CloneSource = false;
        public bool CreatePackages = false;
        public bool Build = false;

        public TestSourceProjectInitializer (
            string originalDirectory,
            string testSourceDirectory
            )
        {
            OriginalDirectory = originalDirectory;
            TestSourceDirectory = testSourceDirectory;
            FileFinder = new FileFinder();
            Executor = new ScriptExecutor (TestSourceDirectory);
        }

        public void Initialize()
        {
            Console.WriteLine("");
            Console.WriteLine("Preparing test source project and package to install from...");
            Console.WriteLine("");
            Console.WriteLine("Original directory:");
            Console.WriteLine("  " + TestSourceDirectory);
            Console.WriteLine("");

            Console.WriteLine("Source project directory:");
            Console.WriteLine("  " + TestSourceDirectory);
            Console.WriteLine("");

            if (CloneSource) {
                new Gitter ().Clone (OriginalDirectory, TestSourceDirectory);
            }

            if (GrabOriginalFiles) {
                new FilesGrabber (
                    OriginalDirectory,
                    TestSourceDirectory
                ).GrabOriginalFiles ();
            }

            var nodes = new ProjectNodeManager (TestSourceDirectory);

            // Refresh the nodes to pick up the status
            nodes.Refresh();

            nodes.CurrentNode.Properties["Version"] = "1.0.0.0";
            nodes.CurrentNode.Save();

            // TODO: Remove if not needed
            //Status = GetStatus();

            if (Build)
            {       
                var buildMode = "Release";

            #if DEBUG
                buildMode = "Debug";
            #endif

                var solutions = nodes.CurrentNode.Properties ["CycleBuildSolutions"];

                new SolutionBuilder(buildMode).BuildAllSolutions(TestSourceDirectory);
            }

            if (CreatePackages) {
                ClearProjectPackages ();

                PrepareProjectPackages ();
            }
        }

        public void ClearProjectPackages()
        {
            foreach (var file in FileFinder.FindFiles(Path.GetFullPath("pkg"), "**.nupkg"))
            {
                var name = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(file));
                if (!name.Contains(".")) // If there's no . in the name then it's a project package and not a 3rd party package
                    File.Delete(file);
            }
        }

        public void PrepareProjectPackages()
        {
            var packager = new NugetPacker ();
            packager.PackAll (TestSourceDirectory);
            //Executor.Execute("CyclePackage", "csAnt");

            // TODO: Remove if not needed
            /*Console.WriteLine("");
            Console.WriteLine("Status: " + Status);
            Console.WriteLine("");*/
        }
    }
}

