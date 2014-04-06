using System;
namespace SoftwareMonkeys.csAnt.SetUp.Common.Tests
{
    public class MockInstallerNugetRetriever : InstallerNugetRetriever
    {
        public MockInstallerNugetRetriever(string source, string destination, Version version)
            : base(
                new MockNugetChecker(false),
                new MockNugetExecutor(source, destination, version)
                )
        {
        }
    }
}

