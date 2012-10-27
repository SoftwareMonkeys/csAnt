using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public bool ScriptExists(string scriptName)
		{
			return !String.IsNullOrEmpty(GetScriptPath(scriptName));
		}
	}
}

