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

        [Test]
        public void Test_EnsureNodes()
        {
            var script = GetDummyScript();

            script.Nodes.EnsureNodes();
            
            var nodeFile = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "csAnt.node";

            Assert.IsTrue(File.Exists(nodeFile), "Node wasn't created.");

            var groupNodeFile = Path.GetDirectoryName(WorkingDirectory)
                + Path.DirectorySeparatorChar
                    + "SoftwareMonkeys.node";

            Assert.IsTrue(File.Exists(groupNodeFile), "Group node wasn't created.");
            
            var sourceNodeFile = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "src"
                + Path.DirectorySeparatorChar
                    + "Source.node";

            Assert.IsTrue(File.Exists(groupNodeFile), "Source node wasn't created.");
        }
    }
}

