using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Tear down
        bool IsTornDown { get;set; }

        IScriptTearDowner TearDowner { get;set; }

        void TearDown();
        #endregion
    }
}

