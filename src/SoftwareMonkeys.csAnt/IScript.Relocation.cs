using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Relocation
        IScriptRelocator Relocator { get;set; }

        void Relocate(string dir);
        #endregion
    }
}

