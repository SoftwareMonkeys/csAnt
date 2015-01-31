using System;
using SoftwareMonkeys.csAnt.Processes;
using System.Linq;

namespace SoftwareMonkeys.csAnt.SourceControl.Git
{
    public class GitBranchIdentifier
    {
        public GitBranchIdentifier ()
        {
        }

        public string Identify()
        {
            var starter = new ProcessStarter ();

            starter.Start ("git branch");

            var output = starter.Output;

            var starPos = output.IndexOf ("*");

            var startRemoved = output.Substring (starPos + 2, output.Length - (starPos + 2));

            var startAndEndRemoved = startRemoved;
            if (startRemoved.Contains(" "))
                startAndEndRemoved = startRemoved.Substring (0, startRemoved.IndexOf (" "));

            var branch = startAndEndRemoved.Trim();

            return branch;
        }
    }
}

