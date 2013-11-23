using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public abstract class BaseScriptingDummyScript : BaseDummyScript
    {
        public override bool Run (string[] args)
        {
            throw new System.NotImplementedException ();
        }

    }
}

