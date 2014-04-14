using System;
using SoftwareMonkeys.csAnt.SourceControl.Git;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Git
        Gitter Git { get;set; }
        #endregion
    }
}

