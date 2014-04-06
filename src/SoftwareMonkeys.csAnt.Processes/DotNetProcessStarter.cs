using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Processes
{
    public class DotNetProcessStarter
    {
        public ProcessStarter Starter { get;set; }

        public bool IsError
        {
            get { return Starter.IsError; }
            set { Starter.IsError = value; }
        }

        public DotNetProcessStarter ()
        {
            Starter = new ProcessStarter();
        }

        public Process Start(string exeFile, params string[] arguments)
        {
            string cmd = exeFile;
            
            List<string> argsList = new List<string>();

            if (Platform.IsMono)
            {
                cmd = "mono";

                // TODO: Re-enable debug flag
                // If the script is in debug mode then use --debug when executing 'mono [program.exe]'
                //if (IsDebug)
                 //   argsList.Add ("--debug");
                argsList.Add(exeFile);
            }

            argsList.AddRange(arguments);

            return Starter.Start(
                cmd,
                argsList.ToArray()
            );
        }
    }
}

