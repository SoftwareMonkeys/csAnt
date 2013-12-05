using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Git
        void GitInit();
        void GitAdd(string filePath);
        void GitCommit();
        #endregion
    }
}

