using System;
using SoftwareMonkeys.csAnt.IO;
using NUnit.Framework;
using System.IO;


namespace SoftwareMonkeys.csAnt.Projects.Tests.Integration
{
    public class SolutionBuildCheckerTestFixture : BaseProjectsIntegrationTestFixture
    {
        [Test]
        public void Test_RequiresBuild_False()
        {
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            var checker = new SolutionBuildChecker();

            // Update the time stamps so it doesn't appear to need a rebuild
            new FileTimeStampManager().Update(CurrentDirectory);

            Assert.IsFalse(checker.RequiresBuild(CurrentDirectory));
        }

        [Test]
        public void Test_RequiresBuild_True()
        {
            new FilesGrabber(
                OriginalDirectory,
                WorkingDirectory
                ).GrabOriginalFiles();

            var checker = new SolutionBuildChecker();

            var timeStampsFile = PathConverter.ToAbsolute("src/TimeStamps.txt");

            if (File.Exists(timeStampsFile))
                File.Delete(timeStampsFile);

            Assert.IsTrue(checker.RequiresBuild(CurrentDirectory));
        }
    }
}

