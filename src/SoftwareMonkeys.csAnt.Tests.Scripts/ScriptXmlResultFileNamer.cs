using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public class ScriptXmlReportFileNamer
    {
        public string GetXmlFileName(ITestScript script)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Getting XML file path...");
            Console.WriteLine ("Current dir:");
            Console.WriteLine (script.CurrentDirectory);
            Console.WriteLine ("");

            var filePath = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + "scripts"
                + Path.DirectorySeparatorChar
                + script.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml";

            foreach (string s in GetTestScriptStack(script)) {
                if (s != script.ScriptName)
                {
                    filePath += Path.DirectorySeparatorChar
                        + s;
                }
            }
            
            filePath += Path.DirectorySeparatorChar
                + script.ScriptName
                + ".xml";

            return filePath;
        }

        public string GetXmlReportsDirectory(ITestScript script)
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
                + "xml";
        }
        
        public Stack<string> GetTestScriptStack(ITestScript script)
        {
            // TODO: Inject via constructor
            var detector = new TestScriptStackDetector(script);

            return detector.Detect();
        }
    }
}

