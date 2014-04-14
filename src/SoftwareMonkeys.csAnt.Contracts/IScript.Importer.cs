using System;
using SoftwareMonkeys.csAnt.Imports;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Import/Export
        Importer Importer { get;set; }
        #endregion
    }
}

