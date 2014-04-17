using System;
namespace SoftwareMonkeys.csAnt.SetUp.Install.Unpack
{
    abstract public class BaseInstallerUnpacker
    {
        public BaseInstallerUnpacker ()
        {
        }

        public abstract void InstallFiles(string projectDirectory, string packageName, Version version, bool overwrite);
    }
}

