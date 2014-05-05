using System;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Unit;


namespace SoftwareMonkeys.csAnt.SetUp.Tests.Unit
{
    public abstract class BaseSetUpUnitTestFixture : BaseUnitTestFixture
    {
        public BaseSetUpUnitTestFixture ()
        {
        }

        public MockInstallerRetriever CreateMockInstallerRetriever(string source, string destination)
        {
            return new MockInstallerRetriever(source, destination);
        }
    }
}

