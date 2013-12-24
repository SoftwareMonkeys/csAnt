using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Start HTTP
        void StartHttp(string dir, string host, int port, bool autoKill);
        #endregion
    }
}

