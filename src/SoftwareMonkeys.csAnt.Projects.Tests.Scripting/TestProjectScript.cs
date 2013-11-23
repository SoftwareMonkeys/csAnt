using System;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
	public class TestProjectScript : BaseProjectDummyScript
	{
        // TODO: Clean up
		public TestProjectScript (string scriptName/*, BaseProjectsTestFixture testFixture*/)
            : base(scriptName/*, testFixture*/)
		{
		}

		public override bool Run (string[] args)
		{
			throw new System.NotImplementedException ();
		}
	}
}

