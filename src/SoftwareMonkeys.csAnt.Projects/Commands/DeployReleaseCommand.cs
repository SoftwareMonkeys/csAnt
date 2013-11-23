using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class DeployReleaseCommand : BaseProjectScriptCommand
    {
        public string ReleaseName { get; set; }

        public string Destination { get;set; }

        public DeployReleaseCommand (BaseProjectScript script, string releaseName, string destination) : base(script)
        {
            ReleaseName = releaseName;
            Destination = destination;
        }

        public override void Execute ()
        {
            Console.WriteLine("");
            Console.WriteLine("Deploying files to a local destination...");
            Console.WriteLine("");

            Console.WriteLine("Destination:");
            Console.WriteLine(Destination);

            var releaseDir = Script.ProjectDirectory
                + Path.DirectorySeparatorChar
                + "rls"
                + Path.DirectorySeparatorChar
                + ReleaseName;

            var releaseFile = Script.GetNewestFile(releaseDir);

            Console.WriteLine("Release file:");
            Console.WriteLine(releaseFile);

            // Unzip the release
            Script.Unzip(releaseFile, Destination);

            // Move the sub directory into the destination
            Script.MoveDirectory(
                Script.GetNewestFolder(Destination),
                Destination
            );

            // Run the prepare script in the new location
            Script.InitializeProject(Destination);
        }
    }
}

