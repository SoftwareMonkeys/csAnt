using System;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    public abstract partial class BaseProjectsTestFixture
    {
        public override IDummyScript GetDummyScript (string scriptName, IScript parentScript, bool isVerbose)
        {
            if (IsVerbose) {
                Console.WriteLine ("Getting dummy script: " + scriptName);
                if (parentScript != null)
                    Console.WriteLine ("Parent script: " + parentScript.ScriptName);

            }

            var testScript = new DummyProjectScript(
                scriptName,
                parentScript
            );

            testScript.IsVerbose = isVerbose;

            testScript.TimeStamp = TimeStamp;
            testScript.Time = Time;

            // TODO: Check if needed
            //Scripts.Add (testScript);

            testScript.EnsureDirectoryExists(WorkingDirectory);

            testScript.CurrentDirectory = WorkingDirectory;

            return testScript;
        }
    }
}

