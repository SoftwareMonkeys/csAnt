using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseScriptingTestFixture : BaseTestFixture
    {
        public BaseScriptingTestFixture ()
        {
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
        
        public virtual ITestScript GetTestScript (string scriptName, IScript parentScript)
        {
            return GetTestScript(scriptName, parentScript, IsVerbose);
        }

        public virtual ITestScript GetTestScript(string scriptName, IScript parentScript, bool isVerbose)
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

            Scripts.Add (testScript);

            testScript.ParentScript = parentScript;

            testScript.CurrentDirectory = WorkingDirectory;

			// TODO: Check if needed. Shouldn't be needed while SetUp is called during Construct
            //testScript.SetUp ();

            return testScript;
        }
    }
}

