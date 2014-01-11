using System;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
	public class TestProjectScript : BaseProjectDummyScript
	{
		public TestProjectScript (string scriptName)
            : base(scriptName)
		{
		}

		public override bool Run (string[] args)
		{
			throw new System.NotImplementedException ();
		}
	}
}

