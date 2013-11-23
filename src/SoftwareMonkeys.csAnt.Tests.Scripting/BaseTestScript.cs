using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public abstract class BaseTestScript : BaseScript, ITestScript
    {
        public TestSummarizer Summarizer { get;set; }

        public string TestGroupName { get;set; }
        
        public ScriptReportGenerator ReportGenerator { get; set; }
        
        public ScriptingTestUtilities Utilities { get;set; }

        public BaseTestScript () : base()
        {
        }

        public BaseTestScript (string scriptName) : base(scriptName)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            Constructor = new ScriptingTestScriptConstructor(this);
            Constructor.Construct(scriptName, parentScript);

            SetUp();
        }
    }
}

