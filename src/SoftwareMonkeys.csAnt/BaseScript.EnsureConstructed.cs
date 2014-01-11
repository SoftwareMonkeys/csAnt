using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void EnsureConstructed (string scriptName)
        {
            if (!IsConstructed) {
                Construct (scriptName);
                IsConstructed = true;
            }
        }
    }
}

