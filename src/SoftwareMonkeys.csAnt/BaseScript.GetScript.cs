using System;
using System.Reflection;
using CSScriptLibrary;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public IScript GetScript(string scriptName)
		{
			var scriptPath = GetScriptPath(scriptName);

			return ActivateScript(scriptPath);
		}
	}
}

