using System;
using System.Reflection;
using CSScriptLibrary;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public IScript ActivateScript(string scriptPath)
		{
			// TODO: Check if needed
			//CSScript.CacheEnabled = true;

			// Load the script assembly
			CSScript.GlobalSettings.DefaultArguments = "/nl"; // TODO: This doesn't seem to be working

			Assembly a = CSScript.Load(scriptPath);

			var scriptName = Path.GetFileNameWithoutExtension(scriptPath);

			// Get the script type
			var type = a.GetTypes () [0];

			var b = (IScript)a.CreateInstance (type.Name);

			// TODO: See if it's possible to pass the scriptName via the constructor when creating the instance above
			b.Initialize(
				scriptName,
				new SubConsoleWriter(Console, scriptName)
			);

			// Create an instance of the script
			IScript script = b;//.AlignToInterface<IScript>(); // TODO: Remove if not needed

			return script;
		}
	}
}

