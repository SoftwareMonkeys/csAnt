using System;
using SoftwareMonkeys.csAnt;

namespace SoftwareMonkeys.csAnt.Projects
{
	public abstract partial class BaseProjectScript : BaseScript
	{
		public BaseProjectScript ()
		{
            Constructor = new ProjectScriptConstructor(this);
		}

		public BaseProjectScript (string scriptName)
		{
            Constructor = new ProjectScriptConstructor(this);

            Construct(scriptName);
		}
        
        public BaseProjectScript (string scriptName, IScript parentScript)
        {
            Constructor = new ProjectScriptConstructor(this);

            Construct(scriptName, parentScript);
        }
	}
}
