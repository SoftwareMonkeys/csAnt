using System;
using System.IO;
using NUnit.Framework;


namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    [TestFixture]
    public class NodesTestFixture : BaseProjectsIntegrationTestFixture
    {
        [Test]
        public void Test_CreateNodes()
        {
            var script = GetDummyScript();

            script.CreateNodes();

            var groupNodeFile = Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                    + "SoftwareMonkeys.node";

            Assert.IsTrue(File.Exists(groupNodeFile), "Group node wasn't created.");
        }
    }
}

