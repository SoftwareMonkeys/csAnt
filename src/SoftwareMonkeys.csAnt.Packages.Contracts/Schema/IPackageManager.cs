using System;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IPackageManager
    {
        IInstallManager Installers { get;set; }

        IRepositoryManager Repositories { get;set; }

        IPackageInfo Create(string workingDirectory, string packageName);
        IPackageInfo Create(string workingDirectory, string packageName, params string[] filePatterns);

        void AddFile(string workingDirectory, string packageName, params string[] filePatterns);

        void RemoveFile(string workingDirectory, string packageName, params string[] filePatterns);

        void Install(string workingDirectory, string packageName);

        void Build(string workingDirectory, string packageName);

        void Send(string workingDirectory, string packageName, string repositoryPath);
    }
}

