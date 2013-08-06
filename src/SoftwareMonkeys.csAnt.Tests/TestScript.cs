using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestScript : BaseTestScript
	{
		public TestScript (BaseTestFixture fixture)
		{
			CurrentDirectory = fixture.GetProjectRoot();
		}

		public override bool Start (string[] args)
		{
			throw new System.NotImplementedException ();
		}
	}
}

