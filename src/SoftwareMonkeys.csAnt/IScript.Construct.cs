using System;

namespace SoftwareMonkeys.csAnt
{
    public partial interface IScript
    {
        #region Construct
        void Construct(string scriptName);

        void Construct(string scriptName, IScript parentScript);
        #endregion
    }
}

