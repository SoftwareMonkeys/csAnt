using System;
using System.Diagnostics;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Start process functions
        Process StartProcess(string command, params string[] arguments);

        Process StartNewProcess(string command, params string[] arguments);
        #endregion
    }
}

