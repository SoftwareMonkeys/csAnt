using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Downloads
        string Download(string downloadUrl, string localDestination);

        void DownloadAndUnzip(string zipFileUrl, string localDestination);

        void DownloadAndUnzip(string zipFileUrl, string zipFileLocalPath, string localDirectory, string subPath, bool force);
        #endregion
    }
}

