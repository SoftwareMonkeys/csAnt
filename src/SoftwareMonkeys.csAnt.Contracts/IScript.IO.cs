using System;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region IO
        string CurrentDirectory { get;set; }

        void MoveDirectory(string from, string to);

        void CopyDirectory(string from, string to);
        void CopyDirectory(string from, string to, bool overwrite);
        
        string GetTmpRoot();

        string GetTemporaryDirectory();
        string GetTmpDir();

        string GetTmpFile();

        void EnsureDirectoryExists(string path);
        
        string[] FindFiles(params string[] patterns);
        string[] FindFiles(string directory, params string[] patterns);

        IFileFinder FileFinder { get; }
        void InitializeFileFinder(IFileFinder finder);

        ITemporaryDirectoryCreator TemporaryDirectoryCreator { get;set; }
        #endregion
    }
}

