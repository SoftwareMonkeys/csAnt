using System;
namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class TestScriptCreator
    {
        public bool IsVerbose { get;set; }

        public string WorkingDirectory { get;set; }
        
        public TestScriptCreator (string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        public TestScriptCreator (string workingDirectory, bool isVerbose)
        {
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

            testScript.ParentScript = parentScript;

            // TODO: Should this be passed as a constructor parameter?
            testScript.CurrentDirectory = WorkingDirectory;

            return testScript;
        }
    }
}

