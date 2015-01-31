using System;
using SoftwareMonkeys.csAnt.Tests.Unit;
using NUnit.Framework;
using SoftwareMonkeys.csAnt.Projects.Tests.Util;

namespace SoftwareMonkeys.csAnt.SourceControl.Git.Tests
{
    public class GitBranchIdentifierTestFixture : BaseUnitTestFixture
    {
        [Test]
        public void Test_Identify()
        {
            var branchName = "TestBranch";

            var initializer = new TestSourceProjectInitializer (OriginalDirectory, WorkingDirectory);
            initializer.CloneSource = true;

            initializer.Initialize ();

            var gitter = new Gitter ();
            gitter.Branch (branchName, true); // Create a new test branch and move to it
            gitter.Branch ("zz"); // Create a branch at the end of the list

            var identifier = new GitBranchIdentifier();
            var identifiedBranch = identifier.Identify ();

            Assert.AreEqual (branchName, identifiedBranch, "Branch doesn't match.");
        }
    }
}

