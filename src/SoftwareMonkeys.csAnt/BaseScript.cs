using System;
using System.IO;
using System.Reflection;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// Used as the base of C# scripts.
	/// </summary>
	public abstract partial class BaseScript : IScript
	{
		public ConsoleWriter Console { get;set; }

		public string ScriptName { get; set; }

		public int Indent { get;set; }

		public BaseScript ()
		{
			var scriptName = GetType ().Name;

			scriptName = FixScriptName(scriptName);

			Initialize(scriptName);
		}

		public BaseScript(string scriptName)
		{
			ScriptName = scriptName;

			Initialize(scriptName);
		}

		public abstract bool Start(string[] args);

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
