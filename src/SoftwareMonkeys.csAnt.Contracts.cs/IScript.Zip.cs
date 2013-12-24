using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Zip functions
        void Zip(string zipFile, params string[] filePatterns);
        
        int Unzip(string zipFile, string destination);

        int Unzip(string zipFile, string destination, string subPath);
        #endregion
    }
}

