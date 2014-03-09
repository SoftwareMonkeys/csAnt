using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Downloads
        void Download(string downloadUrl, string toFile);

        void DownloadAndUnzip(string zipFileUrl, string unzipPath);

        void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force);
        #endregion
    }
}

