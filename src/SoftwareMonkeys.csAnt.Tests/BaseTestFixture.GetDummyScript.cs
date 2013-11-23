using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    public abstract partial class BaseTestFixture
    {
        public virtual IDummyScript GetDummyScript ()
        {
            return GetDummyScript(TestContext.CurrentContext.Test.Name);
        }
        
        public virtual IDummyScript GetDummyScript (bool isVerbose)
        {
            return GetDummyScript(TestContext.CurrentContext.Test.Name, null, isVerbose);
        }
        
        public virtual IDummyScript GetDummyScript (string scriptName)
        {
            return GetDummyScript(scriptName, null, IsVerbose);
        }
        
        public virtual IDummyScript GetDummyScript (string scriptName, IScript parentScript)
        {
            return GetDummyScript(scriptName, parentScript, parentScript.IsVerbose);
        }

        public virtual IDummyScript GetDummyScript (string scriptName, IScript parentScript, bool isVerbose)
        {
            if (IsVerbose) {
                Console.WriteLine ("Getting dummy script: " + scriptName);
                if (parentScript != null)
                    Console.WriteLine ("Parent script: " + parentScript.ScriptName);

            }

            var testScript = new DummyScript(
                scriptName,
                parentScript
            );

            testScript.IsVerbose = isVerbose;

            testScript.TimeStamp = TimeStamp;
            testScript.Time = Time;

            Scripts.Add (testScript);

            testScript.CurrentDirectory = WorkingDirectory;

            return testScript;
        }
    }
}

