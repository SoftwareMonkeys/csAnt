using System;
using SoftwareMonkeys.csAnt;


namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private ScriptFinder scriptFinder;
        public ScriptFinder ScriptFinder
        {
            get
            {
                if (scriptFinder == null)
                    scriptFinder = new ScriptFinder();
                return scriptFinder;
            }
            set { scriptFinder = value; }
        }
    }
}

