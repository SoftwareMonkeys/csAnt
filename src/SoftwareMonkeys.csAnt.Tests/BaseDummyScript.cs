using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseTestScript : BaseScript, ITestScript
	{
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get;set; }

		public TestRelocator Relocator { get;set; }

		public TestUtilities Utilities { get; set; }

		public TestFilesGrabber Grabber { get; set; }

        public DummyScriptConstructor Constructor { get;set; }

        public TestScriptSetUpper SetUpper { get; set; }

        public TestScriptTearDowner TearDowner { get; set; }

		public BaseTestScript () : base()
		{
			Construct();
		}

		public BaseTestScript (string scriptName) : base(scriptName)
		{
			Construct();
		}

		public void Construct ()
		{

            Constructor = new DummyScriptConstructor();
            Constructor.Construct(this);
		}

		public void Fail(string message)
		{
			Error (message);
		}

		public void AssertIsTrue(bool value, string message)
		{
			if (!value)
				Fail (message);
		}

		public override void SetUp()
		{
            SetUpper.SetUp(this, CurrentDirectory);

            // TODO: Remove if not needed
			/*Utilities = new TestUtilities(this);

			StopOnFail = false;
            
            var xmlReportFileNamer = new TestScriptXmlReportFileNamer();
            var htmlReportFileNamer = new TestScriptHtmlReportFileNamer();

			// TODO: Check if these should be injected
			ReportGenerator = new TestScriptReportGenerator(
                this,
                xmlReportFileNamer,
                htmlReportFileNamer,
                new TestScriptHtmlReportGenerator(
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

		public override void TearDown ()
		{
            TearDowner.TearDown(this);

            // TODO: Remove if not needed
			/*base.TearDown ();

            Console.WriteLine ("Is verbose: " + IsVerbose.ToString());

			TestSummarizer.Summarize ();

			ReportGenerator.GenerateReports ();

			Utilities.CopyTestResults(CurrentDirectory, OriginalDirectory);

			if (IsVerbose) {
				Console.WriteLine ("Current directory: " + CurrentDirectory);
				CurrentDirectory = OriginalDirectory;
				Console.WriteLine ("Original directory: " + OriginalDirectory);
			}

			Relocator.Return();*/
            }

        public override void Dispose()
        {
            TearDown();

            base.Dispose();
        }
	}
}

