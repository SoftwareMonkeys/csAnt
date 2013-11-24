using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public interface ITestScript : IScript
    {
        TestSummarizer TestSummarizer { get;set; }

        // TODO: Check if needed
        // The group to put all tests in
        string TestGroupName { get;set; }
    }
}

