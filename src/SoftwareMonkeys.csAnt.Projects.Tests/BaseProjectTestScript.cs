using System;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripts;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripts
{
	public abstract class BaseProjectTestScript : BaseProjectScript, IDummyScript
	{
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get; set; }

		public ScriptingTestUtilities Utilities { get;set; }

		public DummyScriptRelocator Relocator { get;set; }

		public TestFilesGrabber Grabber { get; set; }

        public DummyScriptConstructor Constructor { get; set; }

        public BaseTestFixture TestFixture { get; set; }

        public DummyScriptSetUpper SetUpper { get;set; }

        public DummyScriptTearDowner TearDowner { get;set; }

		public BaseProjectTestScript (BaseTestFixture testFixture) : base()
		{
            TestFixture = testFixture;

            Constructor = new DummyScriptConstructor();
            Constructor.Construct(this); 
		}

		public override abstract bool Start(string[] args);

		public override void SetUp ()
		{
			base.SetUp ();

            SetUpper.SetUp(this, TestFixture.WorkingDirectory);
		}

		public override void TearDown ()
		{
			base.TearDown ();
			
            TearDowner.TearDown(this);
		}
	}
}

