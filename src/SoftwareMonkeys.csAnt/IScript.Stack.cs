using System;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Parent and stack
        IScript ParentScript { get;set; }

        Stack<string> ScriptStack { get;set; }
        #endregion
    }
}

