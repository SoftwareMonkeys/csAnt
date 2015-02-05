using System;
using NUnit.Framework;
using NuGet;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Versions.Tests
{
    [TestFixture]
    public class NugetVersionerTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_VersionMatches_StatusAndBranchMatches()
        {
            var versioner = new NugetVersioner ();

            var version = new SemanticVersion ("1.0.0.0-alpha-branch");

            var versionQuery = new Version ("1.0.0.0");

            var status = "alpha";

            var branch = "branch";

            var matches = versioner.VersionMatches (version, versionQuery, status, branch);

            Assert.IsTrue (matches, "Versions don't match.");
        }

        [Test]
        public void Test_VersionMatches_BranchDoesntMatch()
        {
            var versioner = new NugetVersioner ();
            
            var status = "alpha";

            var branch = "branch";
            
            var version = new SemanticVersion ("1.0.0.0-" + status + "-" + branch);

            var versionQuery = new Version ("1.0.0.0");

            var matches = versioner.VersionMatches (version, versionQuery, status, "differentbranch");

            Assert.IsFalse (matches, "Versions match when they shouldn't.");
        }
        
        [Test]
        public void Test_VersionMatches_StatusDoesntMatch()
        {
            var versioner = new NugetVersioner ();
            
            var status = "alpha";

            var branch = "branch";

            var version = new SemanticVersion ("1.0.0.0-" + status + "-" + branch);

            var versionQuery = new Version ("1.0.0.0");

            var matches = versioner.VersionMatches (version, versionQuery, "differentstatus", branch);

            Assert.IsFalse (matches, "Versions match when they shouldn't.");
        }
    }
}

