using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public class ScriptHtmlReportFileNamer
    {
        public string GetFileName(ITestScript script)
        {
            throw new NotImplementedException();
        }

        public string GetHtmlReportsDirectory(ITestScript script)
        {
            return script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + "scripts"
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "html";
        }
    }
}

