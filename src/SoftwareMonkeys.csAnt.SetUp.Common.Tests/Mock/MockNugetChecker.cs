using System;
using SoftwareMonkeys.csAnt.External.Nuget;


namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.Tests
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

