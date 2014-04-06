using System;
namespace SoftwareMonkeys.csAnt.Projects.Tests.Helpers
{
    abstract public class BaseTestInstallRetriever
    {
        public BaseTestInstallRetriever ()
        {
        }

        public abstract void Retrieve(string source, string destination);
    }
}

