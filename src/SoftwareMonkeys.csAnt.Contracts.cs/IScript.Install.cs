using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Install
        void Install(string name);
        void Install(string name, bool overwriteFiles);
        #endregion
    }
}

