using System;

namespace SoftwareMonkeys.csAnt.IO.Compression
{
    public interface IFileZipper
    {
        void Zip(string workingDirectory, string zipFilePath, params string[] filePatterns);

        int Unzip(string zipFilePath, string destinationDirectory, string subPath);
    }
}

