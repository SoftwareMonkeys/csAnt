using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class ResultFileNamer
    {
        public string GetResultsDirectory(IScript script)
        {
            return script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml";
        }
    }
}

