using System;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IInstallManager
    {
        IInstallInfo Create(string workingDirectory, string name);

        IInstallInfo Load(string workingDirectory, string name);

        void Save(string workingDirectory, IInstallInfo info);

        void AddPackages(string workingDirectory, string installName, params string[] packageName);
    }
}

