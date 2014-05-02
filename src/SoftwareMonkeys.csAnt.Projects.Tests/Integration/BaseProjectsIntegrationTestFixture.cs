using System;
using SoftwareMonkeys.csAnt.Tests;


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

