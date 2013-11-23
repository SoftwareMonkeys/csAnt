using System;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripts
{
	public abstract class BaseProjectTestScript : BaseProjectScript, IDummyScript
	{
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get; set; }

		public DummyScriptRelocator Relocator { get;set; }

        public BaseTestFixture TestFixture { get; set; }

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

