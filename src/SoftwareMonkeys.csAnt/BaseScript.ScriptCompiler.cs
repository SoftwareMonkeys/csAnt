using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        private IScriptCompiler scriptCompiler;
        public IScriptCompiler ScriptCompiler
        {
            get
            {
                if (scriptCompiler == null)
                    scriptCompiler = new ScriptCompiler();
                return scriptCompiler;
            }
            set { scriptCompiler = value; }
        }
    }
}

