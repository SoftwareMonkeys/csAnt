using System;
using Microsoft.Build.BuildEngine;
using SoftwareMonkeys.csAnt.Processes;
using System.Collections.Generic;
using System.IO;


namespace SoftwareMonkeys.csAnt.Projects
{
    public class SolutionBuilder
    {
        public ProcessStarter Starter { get;set; }

        public string BuildMode = "Release";
        
        public SolutionBuilder ()
        {
            Starter = new ProcessStarter();
        }

        public SolutionBuilder (string buildMode)
        {
            BuildMode = buildMode;
            Starter = new ProcessStarter();
        }

        public bool BuildSolution(
            string solutionName
        )
        {
            var srcDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "src";

            var solutionFile = srcDir
                + Path.DirectorySeparatorChar
                    + solutionName.Replace(".sln", "") + ".sln";

            if (!File.Exists(solutionFile))
                throw new IOException("Can't find '" + solutionFile.Replace(Environment.CurrentDirectory, "") + "' solution file.");

            return BuildSolutionFile(solutionFile);
        }
        
        public bool BuildSolutionFile(
            string solutionFilePath
        )
        {
            var success = false;

            if (Platform.IsMono)
            {
                success = RunXBuild(solutionFilePath);
            }
            else
            {
                success = RunMicrosoftBuild(solutionFilePath);
            }

            return success;
        }

        // Move to dedicated msbuild component
        public bool RunMicrosoftBuild(string solutionFilePath)
        {
            var success = false;

            // Instantiate a new Engine object
            Engine engine = new Engine();

            // Point to the path that contains the .NET Framework 4.0 CLR and tools
            engine.BinPath = @"c:\windows\microsoft.net\framework\v4.0.xxxxx";

            // Instantiate a new console logger
            ConsoleLogger logger = new ConsoleLogger();

            // Register the logger with the engine
            engine.RegisterLogger(logger);

            // Create the project object
            var project = new Project(engine);

            // Set the configuration/mode
            project.SetProperty("Configuration", BuildMode);

            // Build the project
            success = project.Build();

            // Unregister all loggers to close the log file
            engine.UnregisterAllLoggers();

            if (success)
                Console.WriteLine("Build succeeded.");
            else
            {
                Console.WriteLine(@"Build failed. View C:\temp\build.log for details");
            }

            return success;
        }

        // TODO: Move to dedicated xbuild component
        public bool RunXBuild(string solutionFilePath)
        {
            var cmdName = "xbuild";

            var arguments = new string[]{
                "\"" + solutionFilePath + "\"",
                "/property:Configuration=" + BuildMode
            };

            var process = Starter.Start(
                cmdName,
                arguments
            );

            process.WaitForExit();

            // TODO: Clean up
        //  var output = cmd.CommandProcess.StandardOutput.ReadToEnd();

        //  var zeroErrorsNotFound = (output.IndexOf("0 Error(s)") == -1);

            var exitCodeSuccess = (process.ExitCode == 0);

            if (
                exitCodeSuccess
            //    && zeroErrorsNotFound
            )

            {
                Console.WriteLine("Build succeeded.");

                return true;
            }
            else
            {
                Console.WriteLine ("Build failed.");

                return false;
            }
        }
        
        public bool BuildAllSolutions(string directory)
        {
            return BuildAllSolutions(directory, "Release");
        }

        public bool BuildAllSolutions (string directory, string mode)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Building all solutions...");
            Console.WriteLine ("");
            Console.WriteLine ("Mode: " + mode);
            Console.WriteLine ("");

            bool success = true;

            int successful = 0;
            int failed = 0;
            int total = 0;

            List<string> failedSolutions = new List<string> ();

            foreach (string slnFile in Directory.GetFiles(directory, "*.sln", SearchOption.AllDirectories)) {
                if (success) {
                    total++;

                    if (BuildSolutionFile (slnFile))
                        successful++;
                    else {
                        failed++;
                        success = false;
                    }
                }

                if (!success) {
                    failedSolutions.Add (slnFile);
                    break;
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Build finished!");
            Console.WriteLine ("");
            Console.WriteLine ("Total: " + total);
            Console.WriteLine ("Successful: " + successful);
            Console.WriteLine ("Failed: " + failed);
            Console.WriteLine ("");

            if (failedSolutions.Count > 0) {
                Console.WriteLine ("The following solutions failed to build:");
                foreach (string failedSolution in failedSolutions) {
                    Console.WriteLine ("  " + failedSolution.Replace (Environment.CurrentDirectory, ""));
                }
            }
            
            Console.WriteLine ("");

            if (failed > 0)
                Console.WriteLine(failed + " solution(s) failed to build.");

            return success;
        }
    }
}

