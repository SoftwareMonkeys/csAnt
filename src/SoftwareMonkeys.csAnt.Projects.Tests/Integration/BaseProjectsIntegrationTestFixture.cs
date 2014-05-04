using System;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Integration;


namespace SoftwareMonkeys.csAnt.Projects.Tests.Integration
{
    public class BaseProjectsIntegrationTestFixture : BaseIntegrationTestFixture
    {
        public BaseProjectsIntegrationTestFixture()
        {
            Nodes = new ProjectNodeManager();
        }
    }
}

