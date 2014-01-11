using System;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IPackageManager
    {
        IRepositoryManager Repositories { get;set; }

        IPackageInfo Create(string packageName, string groupName);
        IPackageInfo Create(string packageName, string groupName, params string[] filePatterns);

        void AddFile(string packageName, string groupName, params string[] filePatterns);

        void RemoveFile(string packageName, string groupName, params string[] filePatterns);

        void Install(string packageName, string groupName);
        
        void Build(string packageName, string groupName);

        void Build(string packageName, string groupName, string version);

        void Send(string packageName, string groupName, string repositoryPath);
    }
}

