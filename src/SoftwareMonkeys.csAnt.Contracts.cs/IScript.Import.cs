using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Import/Export
        string ImportStagingDirectory { get;set; }

        string GetImportStagingDirectory();

        void ImportFile(string projectName, string filePattern);

        void ExportFile(string projectName, string filePattern);
        #endregion
    }
}

