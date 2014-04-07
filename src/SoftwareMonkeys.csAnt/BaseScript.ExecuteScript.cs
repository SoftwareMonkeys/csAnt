using System;
using System.Collections.Generic;
using System.IO;
using CSScriptLibrary;
using System.Reflection;
using System.Text;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		public void ExecuteScript(string scriptName)
		{
			ExecuteScript(scriptName, new string[]{});
		}
		
		public void ExecuteScript (string scriptName, params string[] args)
        {
            var script = ScriptExecutor.Activator.ActivateScript(scriptName);
         
            ExecuteScript(script, args);
        }

        public IScript ExecuteScript (IScript script, params string[] args)
        {
            return ScriptExecutor.Execute(script, args);
        }
    }
}
