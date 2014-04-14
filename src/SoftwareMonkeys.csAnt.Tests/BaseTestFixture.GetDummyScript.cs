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

            var script = new DummyScript (
                scriptName,
                parentScript
            );

            script.Nodes = Nodes;

            script.IsVerbose = isVerbose;

            script.TimeStamp = TimeStamp;
            script.Time = Time;
            
            if (IsVerbose) {
                Console.WriteLine ("Time stamp: " + TimeStamp);
                Console.WriteLine ("Time: " + Time.ToString ());
            }

            Scripts.Add (script);

            script.CurrentDirectory = WorkingDirectory;

            return script;
        }
    }
}

