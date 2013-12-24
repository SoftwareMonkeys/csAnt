using System;

namespace SoftwareMonkeys.csAnt
{
    public interface ITemporaryDirectoryCreator
    {
        string GetTmpDir();
        string GetTmpRoot();
    }
}

