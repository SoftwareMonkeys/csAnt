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
			if (String.IsNullOrEmpty(scriptName))
				throw new ArgumentException("The script name must be provided.", "scriptName");

			ScriptName = scriptName;

			Initialize(scriptName);
		}
	}
}
