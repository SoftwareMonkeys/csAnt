using System;
namespace SoftwareMonkeys.csAnt.SetUp.Install.Unpack
{
    abstract public class BaseInstallUnpacker
    {
        public BaseInstallUnpacker ()
        {
        }

        public abstract void Unpack(string projectDirectory, string packageName, Version version, bool overwrite);
    }
}

