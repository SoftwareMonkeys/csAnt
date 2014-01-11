using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Time
        string TimeStamp { get;set; }

        DateTime Time { get;set; }

        string GetTimeStamp();
        string GetTimeStamp(DateTime dateTime);
        #endregion
    }
}

