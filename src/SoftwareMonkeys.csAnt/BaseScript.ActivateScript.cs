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
			CSScript.GlobalSettings.DefaultArguments = "/nl"; // TODO: This doesn't seem to be working

			var assemblyFile = CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "bin"
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
				+ Path.GetFileName(scriptPath)
				+ ".dll";

			EnsureDirectoryExists(Path.GetDirectoryName(assemblyFile));

			// Load the script assembly
			Assembly a = CSScript.Load(scriptPath, assemblyFile, IsDebug, new string[]{});

			var scriptName = Path.GetFileNameWithoutExtension(scriptPath);

			// Get the script type
			var type = a.GetTypes () [0];

			var s = (IScript)a.CreateInstance (type.Name);

			// TODO: See if it's possible to pass the scriptName via the constructor when creating the instance above
			s.Initialize(
				scriptName,
				new SubConsoleWriter(Console, scriptName)
			);

			// Create an instance of the script
			IScript script = s;

			return script;
		}
	}
}

