using System;

namespace SoftwareMonkeys.csAnt.IO
{
    public interface IFileFinder
    {
        string[] FindFiles(string directory, params string[] patterns);
    }
}

