using System;
using SoftwareMonkeys.csAnt.Tests;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	public class BaseProjectsTestFixture : BaseTestFixture
	{
		public BaseProjectsTestFixture ()
		{
		}

		public new TestProjectScript GetTestScript()
		{
			var testScript = new TestProjectScript();

			InitializeTestScript(testScript);

			return testScript;
		}
	}
}

