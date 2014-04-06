using System;


namespace SoftwareMonkeys.csAnt.SetUp
{
    abstract public class BaseDeploymentFilesRetriever
    {
        public BaseDeploymentFilesRetriever ()
        {
        }

        public abstract void Retrieve(string source, string destination);
    }
}

