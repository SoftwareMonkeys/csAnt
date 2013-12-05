using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Sync
        void Sync(string dir1, string dir2);
        #endregion
    }
}

