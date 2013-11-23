using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
    public abstract class BaseScriptingTestScript : BaseScript, ITestScript
    {
        public TestSummarizer Summarizer { get;set; }

        public string TestGroupName { get;set; }
        
        public ScriptReportGenerator ReportGenerator { get; set; }
        
        public ScriptingTestUtilities Utilities { get;set; }

        public BaseScriptingTestScript () : base()
        {
            Construct();
        }

        public BaseScriptingTestScript (string scriptName) : base(scriptName)
        {
            Construct();
        }

        public void Construct ()
        {
            Constructor = new ScriptingTestScriptConstructor(this);
            Constructor.Construct();
        }
    }
}

