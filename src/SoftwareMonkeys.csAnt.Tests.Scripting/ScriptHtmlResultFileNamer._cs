using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class ScriptHtmlResultFileNamer
    {
        public string GetFileName(ITestScript script)
        {
            throw new NotImplementedException();
        }

        public string GetHtmlResultsDirectory(ITestScript script)
        {
            return script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html";
        }
    }
}

