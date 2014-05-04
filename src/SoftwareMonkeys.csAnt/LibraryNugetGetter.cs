using System;
using System.IO;
using SoftwareMonkeys.csAnt.Processes;
using SoftwareMonkeys.csAnt.External.Nuget;
using SoftwareMonkeys.FileNodes;


namespace SoftwareMonkeys.csAnt
{
    // TODO: Decide whether this should be moved to External.Nuget assembly
    public class LibraryNugetGetter
    {
        public IFileNodeState NodeState { get;set; }
        public DotNetProcessStarter Starter { get;set; }
        public NugetChecker Checker { get;set; }

        public LibraryNugetGetter (IFileNodeState nodeState)
        {
            NodeState = nodeState;
            Starter = new DotNetProcessStarter();
            Checker = new NugetChecker();
        }

        public bool Get(string name)
        {
            
            Console.WriteLine ("");
            Console.WriteLine ("Installing from nuget: " + name);
            Console.WriteLine ("");

            // Check that nuget is installed
            Checker.CheckNuget();

            var libNode = NodeState.CurrentNode.Nodes["Libraries"].Nodes[name];

            var packageName = libNode.Properties["nuget"];

            var nugetExe = "nuget.exe";

            // Move into the lib directory
            Environment.CurrentDirectory = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "lib";

            Starter.Start(nugetExe, "install " + packageName);

            // Move back to the project directory
            Environment.CurrentDirectory = Path.GetDirectoryName(Environment.CurrentDirectory);

            return true;
        }
    }
}

