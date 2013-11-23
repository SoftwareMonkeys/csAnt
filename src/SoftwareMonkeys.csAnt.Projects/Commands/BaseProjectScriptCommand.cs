using System;
using SoftwareMonkeys.csAnt.Commands;

namespace SoftwareMonkeys.csAnt.Projects
{
    public abstract class BaseProjectScriptCommand : BaseScriptCommand
    {
        public new BaseProjectScript Script {
            get {
                return (BaseProjectScript)base.Script;
            }
        }

        public BaseProjectScriptCommand (BaseProjectScript script) : base(script)
        {
        }
    }
}

