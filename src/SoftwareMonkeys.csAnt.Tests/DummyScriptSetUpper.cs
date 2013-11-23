using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class TestScriptSetUpper
    {
        public void SetUp (ITestScript script, string workingDirectory)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Setting up test script...");
            Console.WriteLine ("Script name:");
            Console.WriteLine (script.ScriptName);
            Console.WriteLine ("Working directory:");
            Console.WriteLine (workingDirectory);

            if (script.IsVerbose) {
                Console.WriteLine ("Test group name:");
                Console.WriteLine (script.TestGroupName);
            }
            // Grab the node file
            script.Grabber.GrabOriginalFiles(workingDirectory, "*.node", "../*.node");

            script.TestGroupName = script.ScriptName;

            script.OriginalDirectory = script.GetOriginalDirectory();

            if (script.IsVerbose)
                Console.WriteLine ("Original directory: " + script.OriginalDirectory);

            script.TestGroupName = script.ScriptName;

            script.Relocator.Relocate(testFixture.WorkingDirectory);

            
            
            // TODO: Remove if not needed
            /*Utilities = new TestUtilities(this);

            StopOnFail = false;
            
            var xmlReportFileNamer = new ScriptXmlReportFileNamer();
            var htmlReportFileNamer = new ScriptHtmlReportFileNamer();

            // TODO: Check if these should be injected
            ReportGenerator = new ScriptReportGenerator(
                this,
                xmlReportFileNamer,
                htmlReportFileNamer,
                new ScriptHtmlReportGenerator(
                    this,
                    xmlReportFileNamer,
                    htmlReportFileNamer
                )
            );
            
            TestSummarizer = new TestSummarizer(this);

            // Set the group name to the name of the script. If tests are executed within the script they'll be placed in that group.
            TestGroupName = ScriptName;

            if (IsVerbose)
                Console.WriteLine ("Original directory: " + OriginalDirectory);

            Relocator.Relocate();

            base.SetUp();*/
        }
    }
}

