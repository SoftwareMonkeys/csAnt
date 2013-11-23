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
			var script = GetDummyScript();

			script.FilesGrabber.GrabOriginalFiles();

            // Break a file so it won't build
            var brokenFile = script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "src"
                + Path.DirectorySeparatorChar
                + "SoftwareMonkeys.csAnt.Tests"
                + Path.DirectorySeparatorChar
                    + "HelloWorldTestFixture.cs";

            var content = File.ReadAllText(brokenFile);

            content = content.Replace ("WriteLine", "WriteLines");

            File.WriteAllText(brokenFile, content);

			script.ExecuteScript("ClearDlls");
			script.ExecuteScript("BuildAllSolutions");

			Assert.IsTrue (script.IsError, "The IsError flag should be true.");
		}
	}
}

