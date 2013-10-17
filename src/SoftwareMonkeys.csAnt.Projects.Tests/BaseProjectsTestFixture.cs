using System;
using SoftwareMonkeys.csAnt.Tests;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	public class BaseProjectsTestFixture : BaseTestFixture
	{
		public string ProjectDirectory { get; set; }

		public BaseProjectsTestFixture ()
		{
			ProjectDirectory = Path.GetFullPath("../../../../");
		}

		public TestProjectScript GetTestScript()
		{
			var testScript = new TestProjectScript();
			
			testScript.IsVerbose = true;

			var actualProjectDir = ProjectDirectory;

			testScript.ProjectDirectory = testScript.GetTmpDir();

			testScript.CopyTestFiles(actualProjectDir, testScript.ProjectDirectory);

			return testScript;
		}
	}
}

