using System;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	public abstract class BaseProjectTestScript : BaseProjectScript, ITestScript
	{
		public string OriginalDirectory { get;set; }

		public TestReportGenerator ReportGenerator { get;set; }
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get; set; }

		public TestUtilities Utilities { get;set; }

		public TestRelocator Relocator { get;set; }

		public TestFilesGrabber Grabber { get; set; }

		public BaseProjectTestScript () : base()
		{
			IsVerbose = true;
			StopOnFail = false;

			// TODO: Check if these should be injected
			ReportGenerator = new TestReportGenerator(this);
			TestSummarizer = new TestSummarizer(this);
			
			Grabber = new TestFilesGrabber(this);
			
			Relocator = new TestRelocator(this);
		}

		public override abstract bool Start(string[] args);

		public override void SetUp ()
		{
			base.SetUp ();

			TestGroupName = ScriptName;
			
			Utilities = new TestUtilities(this);

			OriginalDirectory = CurrentDirectory;

			if (IsVerbose)
				Console.WriteLine ("Actual directory: " + OriginalDirectory);

			Relocator.Relocate();
		}

		public override void TearDown ()
		{
			base.TearDown ();
			
			TestSummarizer.Summarize();

			ReportGenerator.GenerateReports();
			
			if (IsVerbose) {
				Console.WriteLine ("Current directory: " + CurrentDirectory);
				CurrentDirectory = OriginalDirectory;
				Console.WriteLine ("Actual directory: " + OriginalDirectory);
			}

			Relocator.Return();
		}
	}
}

