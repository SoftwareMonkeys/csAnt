using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Processes
{
    public class DotNetProcessStarter : ProcessStarter
    {
        public DotNetProcessStarter ()
        {
        }

        public override Process Start(string exeFile, params string[] arguments)
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

            return base.Start(
                cmd,
                argsList.ToArray()
            );
        }
    }
}

