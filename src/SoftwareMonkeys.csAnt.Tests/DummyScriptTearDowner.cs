using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class TestScriptTearDowner
    {
        public void TearDown(ITestScript script)
        {
            Console.WriteLine ("Is verbose: " + script.IsVerbose.ToString());

            script.TestSummarizer.Summarize();

            script.ReportGenerator.GenerateReports();
            
            script.Utilities.CopyTestResults(script.CurrentDirectory, script.OriginalDirectory);

            if (script.IsVerbose) {
                Console.WriteLine ("Current directory: " + script.CurrentDirectory);
                Console.WriteLine ("Actual directory: " + script.OriginalDirectory);
            }

            script.CurrentDirectory = script.OriginalDirectory;

            script.Relocator.Return();
        }
    }
}

