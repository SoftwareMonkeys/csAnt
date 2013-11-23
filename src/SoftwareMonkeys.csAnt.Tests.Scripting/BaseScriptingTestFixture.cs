using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseScriptingTestFixture : BaseTestFixture
    {
        public BaseScriptingTestFixture ()
        {
        }
        
        public ScriptHtmlResultGenerator GetHtmlReportGenerator(ITestScript script)
        {
            var htmlReportFileNamer = new ScriptHtmlResultFileNamer();
            var xmlReportFileNamer = new ScriptXmlResultFileNamer();

            var generator = new ScriptHtmlResultGenerator(
                script,
                xmlReportFileNamer,
                htmlReportFileNamer
            );

            return generator;
        }

        public ScriptReportGenerator GetReportGenerator(ITestScript script)
        {
            var htmlGenerator = GetHtmlReportGenerator(script);

            var generator = new ScriptReportGenerator(
                script,
                htmlGenerator.XmlResultFileNamer,
                htmlGenerator.HtmlResultFileNamer,
                htmlGenerator
            );

            return generator;
        }
        
        
        public virtual ITestScript GetTestScript ()
        {
            return GetTestScript("TestScript");
        }
        
        public virtual ITestScript GetTestScript (bool isVerbose)
        {
            return GetTestScript("TestScript", null, isVerbose);
        }
        
        public virtual ITestScript GetTestScript (string scriptName)
        {
            return GetTestScript(scriptName, null, IsVerbose);
        }
        
        public virtual ITestScript GetTestScript (string scriptName, ITestScript parentScript)
        {
            return GetTestScript(scriptName, parentScript, IsVerbose);
        }

        public virtual ITestScript GetTestScript(string scriptName, ITestScript parentScript, bool isVerbose)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Getting test script: " + scriptName);

            if (parentScript != null)
                Console.WriteLine ("Parent script: " + parentScript.ScriptName);

            Console.WriteLine ("");

            var testScript = new ScriptingTestScript(
                scriptName
            );

            Scripts.Add (testScript);

            testScript.ParentScript = parentScript;

            testScript.CurrentDirectory = WorkingDirectory;

            //testScript.SetUp ();

            return testScript;
        }
    }
}

