using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        
        #region Events
        void RaiseEvent(string eventName);

        string[] GetEventScripts(string eventName);
        #endregion
    }
}

