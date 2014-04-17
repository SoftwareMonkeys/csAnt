using System;


namespace SoftwareMonkeys.csAnt.SetUp.Install.Retrieve
{
    abstract public class BaseDeploymentFilesRetriever
    {
        public BaseDeploymentFilesRetriever ()
        {
        }

        public abstract void Retrieve(string source, string destination);
    }
}

