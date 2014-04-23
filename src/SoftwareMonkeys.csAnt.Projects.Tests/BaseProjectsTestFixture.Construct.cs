using System;
namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    public partial class BaseProjectsTestFixture
    {
        public override void Construct()
        {
            Nodes = new ProjectNodeManager();
        }
    }
}

