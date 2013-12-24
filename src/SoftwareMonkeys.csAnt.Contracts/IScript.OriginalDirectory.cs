using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Original directory
        string GetOriginalDirectory();

        string OriginalDirectory { get;set; }
        #endregion
    }
}

