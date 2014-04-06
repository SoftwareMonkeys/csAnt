using System;
namespace SoftwareMonkeys.csAnt.SetUp
{
    abstract public class BaseInstallerFileManager
    {
        public BaseInstallerFileManager ()
        {
        }

        public abstract void InstallFiles(string projectDirectory, string packageName, Version version, bool overwrite);
    }
}

