using System;

namespace SoftwareMonkeys.csAnt.Packages.Schema
{
    public interface IRepositoryManager
    {
        IRepositoryInfo Create(string name, string path);

        IRepositoryInfo Load(string name);

        void Save(IRepositoryInfo repository);
    }
}

