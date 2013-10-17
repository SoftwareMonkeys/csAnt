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

			return GetScriptFromPath(scriptPath);
		}
		
		public IScript GetScriptFromPath(string scriptPath)
		{
			// TODO: Clean up
			// Load the script assembly
			Assembly a = CSScript.Load (scriptPath);

			var scriptName = Path.GetFileNameWithoutExtension(scriptPath);

			// Get the script type
			var type = a.GetTypes () [0];

			var b = (IScript)a.CreateInstance (type.Name);

			// TODO: See if it's possible to pass the scriptName via the constructor when creating the instance above
			b.Initialize(scriptName, Console);

			// Create an instance of the script
			IScript script = b;//.AlignToInterface<IScript>(); // TODO: Remove if not needed

			return script;
		}
	}
}

