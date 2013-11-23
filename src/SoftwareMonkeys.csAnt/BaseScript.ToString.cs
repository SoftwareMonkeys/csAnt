using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public override string ToString ()
        {
            return string.Format (GetType().Name + " - '" + ScriptName + "'");
        }
    }
}

