using System;
namespace SoftwareMonkeys.csAnt.SetUp.Install
{
    abstract public class BaseInstaller
    {
        public string DestinationPath { get;set; }

        public BaseInstaller ()
        {
            DestinationPath = Environment.CurrentDirectory;
        }

        abstract public void Install();
    }
}

