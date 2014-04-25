using System;
namespace SoftwareMonkeys.csAnt.SetUp.Install.Retrieve
{
    abstract public class BaseInstallerRetriever
    {
        public BaseInstallerRetriever ()
        {
        }
        
        public abstract void Retrieve(string packageName);
        public abstract void Retrieve(string packageName, Version version, string status);
    }
}

