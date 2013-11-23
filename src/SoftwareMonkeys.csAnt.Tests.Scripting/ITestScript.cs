using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public interface ITestScript : IScript
    {
        // The group to put all tests in
        string TestGroupName { get;set; }

        TestSummarizer Summarizer { get;set; }
        
        ScriptReportGenerator ReportGenerator { get;set; }

        ScriptingTestUtilities Utilities { get;set; }
    }
}

