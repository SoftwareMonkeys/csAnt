using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Error
        bool StopOnFail { get;set; }

        bool IsError { get;set; }

        void Error(string message);
        void Error(string message, Exception ex);
        void Error(Exception ex);
        #endregion
    }
}

