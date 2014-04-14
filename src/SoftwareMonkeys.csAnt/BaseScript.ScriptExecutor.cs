using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private IScriptExecutor scriptExecutor;
        public IScriptExecutor ScriptExecutor
        {
            get
            {
                if (scriptExecutor == null)
                    scriptExecutor = new ScriptExecutor(this);
                return scriptExecutor;
            }
            set { scriptExecutor = value; }
        }
    }
}

