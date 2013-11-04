using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseTestScript : BaseScript, ITestScript
	{
		public string OriginalDirectory { get;set; }

		public TestSummarizer TestSummarizer { get;set; }

		public TestReportGenerator ReportGenerator { get;set; }

		public string TestGroupName { get;set; }

		public TestRelocator Relocator { get;set; }

		public TestUtilities Utilities { get; set; }

		public TestFilesGrabber Grabber { get; set; }

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
			Relocator = new TestRelocator(this);
			Grabber = new TestFilesGrabber(this);
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
			Utilities = new TestUtilities(this);

			IsVerbose = true;
			StopOnFail = false;

			// TODO: Check if these should be injected
			ReportGenerator = new TestReportGenerator(this);
			TestSummarizer = new TestSummarizer(this);

			// Set the group name to the name of the script. If tests are executed within the script they'll be placed in that group.
			TestGroupName = ScriptName;

			OriginalDirectory = CurrentDirectory;

			if (IsVerbose)
				Console.WriteLine ("Actual directory: " + OriginalDirectory);

			Relocator.Relocate();

			base.SetUp();
		}

		public override void TearDown ()
		{
			base.TearDown ();

			TestSummarizer.Summarize ();

			ReportGenerator.GenerateReports ();

			if (IsVerbose) {
				Console.WriteLine ("Current directory: " + CurrentDirectory);
				CurrentDirectory = OriginalDirectory;
				Console.WriteLine ("Actual directory: " + OriginalDirectory);
			}

			Relocator.Return();
		}

		public override string GetTmpRoot ()
		{
			return base.GetTmpRoot ();
		}
	}
}

