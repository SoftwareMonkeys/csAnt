using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Build mode
        bool IsDebug { get;set; }

        string GetBuildMode();
        #endregion
    }
}

