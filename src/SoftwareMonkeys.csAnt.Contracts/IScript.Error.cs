using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Error
        bool StopOnFail { get;set; }

        bool IsError { get;set; }

        void Error(string message);
        #endregion
    }
}

