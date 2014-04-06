using System;
namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    abstract public class BaseInstallerRetriever
    {
        public BaseInstallerRetriever ()
        {
        }

        public abstract void Retrieve(string source, string destination);
    }
}

