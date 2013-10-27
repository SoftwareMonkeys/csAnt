using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
	[TestFixture]
	public class BuildAllSolutionsTestFixture : BaseProjectsTestFixture
	{
		[Test]
		public void Test_BuildAllSolutions_HandleError()
		{
			var script = GetTestScript();

			GrabOriginalScriptingFiles(script);

			script.ExecuteScript("Clean");
			script.ExecuteScript("BuildAllSolutions");

			Assert.IsTrue (script.IsError, "The IsError flag should be true.");
		}
	}
}

