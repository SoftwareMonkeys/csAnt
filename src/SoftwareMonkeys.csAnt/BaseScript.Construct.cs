using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public virtual void Construct(string scriptName)
        {
            Construct(scriptName, null);
        }

        public virtual void Construct(string scriptName, IScript parentScript)
        {
            Constructor.Construct(scriptName, parentScript);

            SetUp();
        }
    }
}

