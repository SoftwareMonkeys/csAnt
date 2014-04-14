using System;
namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class TestScriptCreator
    {
        public bool IsVerbose { get;set; }

        public string OriginalDirectory { get;set; }

        public string WorkingDirectory { get;set; }
        
        public TestScriptCreator (string originalDirectory, string workingDirectory)
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
        }

        public TestScriptCreator (string originalDirectory, string workingDirectory, bool isVerbose)
        {
            OriginalDirectory = originalDirectory;
            WorkingDirectory = workingDirectory;
            IsVerbose = isVerbose;
        }

        public ITestScript Create()
        {
            return Create("TestScript");
        }
        
        public ITestScript Create(string scriptName)
        {
            return Create(scriptName, null);
        }

        public ITestScript Create(string scriptName, IScript parentScript)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Getting test script: " + scriptName);

            if (parentScript != null)
                Console.WriteLine ("Parent script: " + parentScript.ScriptName);

            Console.WriteLine ("");

            var testScript = new ScriptingTestScript(
                scriptName,
                parentScript
            );

            if (parentScript == null)
            {
                testScript.OriginalDirectory = OriginalDirectory;
                testScript.CurrentDirectory = WorkingDirectory;
            }

            return testScript;
        }
    }
}

