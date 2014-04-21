using System;
using System.Collections.Generic;
using System.IO;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.Projects
{
    public class SolutionBuildChecker
    {
        public ScriptExecutor Executor { get;set; }

        public string CycleBuildScript = "CycleBuild";

        public FileTimeStampManager TimeStamps { get;set; }

        public SolutionBuildChecker ()
        {
            Executor = new ScriptExecutor();
            TimeStamps = new FileTimeStampManager();
        }

        public bool RequiresBuild(string projectDirectory)
        {
            var previousTimeStamps = TimeStamps.GetPreviousData(projectDirectory);
    
            var newTimeStamps = TimeStamps.GetNewData(projectDirectory);
    
            var needsBuild = false;
    
            // If there's a different number of files then rebuild
            if (previousTimeStamps.Count != newTimeStamps.Count)
            {
                Console.WriteLine("Different number of files detected... needs rebuild...");
                Console.WriteLine("Previous number: " + previousTimeStamps.Count);
                Console.WriteLine("New number: " + newTimeStamps.Count);
    
                needsBuild = true;
            }
            else
            {
                foreach (KeyValuePair<string, string> entry in previousTimeStamps)
                {
                    var key = entry.Key;
    
                    var timeStamp1 = previousTimeStamps[key];
    
                    var timeStamp2 = newTimeStamps[key];
    
                    if (timeStamp1 != timeStamp2)
                    {
                        Console.WriteLine("File has changed:");
                        Console.WriteLine(key);
    
                        Console.WriteLine("");
                        Console.WriteLine("Launching build cycle (CycleBuild script)");
    
                        needsBuild = true;
                        break;
                    }
                }
            }

            return needsBuild;
        }

        public void EnsureBuilt(string projectDirectory, string buildMode)
        {
            if (RequiresBuild(projectDirectory))
            {
                Build(projectDirectory, buildMode);
            }
            else
                Console.WriteLine("All builds are up to date. Skipping build.");
        }

        public void Build(string projectDirectory, string buildMode)
        {
            Build(projectDirectory, buildMode, TimeStamps.GetNewData(projectDirectory));
        }

        public void Build(string projectDirectory, string buildMode, Dictionary<string, string> latestTimeStamps)
        {
            Executor.WorkingDirectory = projectDirectory;

            // Execute the "CycleBuild" script (or whatever was specified)
            Executor.Execute(CycleBuildScript, "-mode=" + buildMode);

            TimeStamps.WriteNewData(projectDirectory, latestTimeStamps);
        }

    }
}

