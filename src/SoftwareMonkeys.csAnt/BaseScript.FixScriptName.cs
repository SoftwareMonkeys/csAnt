using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public string FixScriptName(string scriptName)
		{
			var x = "Script";

			// If script name ends with "Script" then remove it
			if (scriptName.EndsWith (x))
				scriptName = scriptName.Substring(0, scriptName.Length-x.Length);

			return scriptName;
		}
	}
}

