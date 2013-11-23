using System;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
    public abstract class BaseProjectTestScript : BaseProjectScript, ITestScript
    {
        public TestSummarizer Summarizer { get;set; }

        public string TestGroupName { get;set; }
        
        public ScriptReportGenerator ReportGenerator { get; set; }
        
        public ScriptingTestUtilities Utilities { get;set; }

        public BaseProjectTestScript () : base()
        {
        }

        public BaseProjectTestScript (string scriptName) : base(scriptName)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            Constructor = new ProjectTestScriptConstructor(this);
            Constructor.Construct(scriptName, parentScript);

            SetUp();
        }
    }
}

