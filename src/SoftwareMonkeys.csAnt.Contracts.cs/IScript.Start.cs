using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Start
        bool Start(params string[] args);
        #endregion
    }
}

