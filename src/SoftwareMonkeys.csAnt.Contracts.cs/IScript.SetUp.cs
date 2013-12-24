using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Set up
        bool IsSetUp { get;set; }

        IScriptSetUpper SetUpper { get;set; }

        void SetUp();
        #endregion
    }
}

