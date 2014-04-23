using System;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ImproviseScript : BaseProjectScript
{
        public static void Main(string[] args)
        {
            new ImproviseScript().Start(args);
        }

        public override bool Run(string[] args)
        {
            var executed = false;

            if (!executed)
                CheckForBuild();

            // TODO: Package if not packaged

            // TODO: Publish if not published

            // TODO: Test if not tested

            // TODO: Generate docs if not found

            if (!executed)
            {
                Console.WriteLine("Can't find any pending tasks to perform. Seems like everything is up to date.");
            }

            return !IsError;
        }

        public bool CheckForBuild()
        {
            var ensurer = new SolutionBuildChecker();
            
            if (ensurer.RequiresBuild(CurrentDirectory))
            {
                ensurer.Build(CurrentDirectory, "Release"); // TODO: Make build mode configurable
                return true;
            }
            else
                return false;
        }
}
