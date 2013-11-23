using System;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    public class DummyProjectScript : BaseProjectScript, IDummyScript
    {
        public TestSummarizer TestSummarizer { get;set; }

        public string TestGroupName { get;set; }

        public DummyProjectScript ()
        {
        }
        
        public DummyProjectScript (string scriptName) : base(scriptName)
        {
        }

        public DummyProjectScript(string scriptName, IScript parentScript) : base(scriptName, parentScript)
        {
        }

        public override bool Run (string[] args)
        {
            throw new System.NotImplementedException ();
        }
    }
}

