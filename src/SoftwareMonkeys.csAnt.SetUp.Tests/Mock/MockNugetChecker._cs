using System;
using SoftwareMonkeys.csAnt.External.Nuget;


namespace SoftwareMonkeys.csAnt.SetUp.Common.Tests
{
    public class MockNugetChecker : NugetChecker
    {
        public bool CheckForNuget = true;

        public MockNugetChecker ()
        {
        }

        public override void CheckNuget()
        {
            if (CheckForNuget)
                base.CheckNuget();
        }
    }
}

