using System;
using System.IO;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.UI.Web
{
	public class ScriptUtilities
	{
		public ScriptUtilities ()
		{
		}

		public string[] GetScripts ()
		{
			List<string> scripts = new List<string> ();

			var scriptsDir = GetScriptsDirectory();

			foreach (var script in Directory.GetFiles(scriptsDir)) {
				scripts.Add (script);
			}

			foreach (var script in Directory.GetDirectories(scriptsDir)) {
				scripts.Add (script);
			}

			return scripts.ToArray ();
		}

		public string GetScriptName(string scriptPath)
		{
			return Path.GetFileNameWithoutExtension(scriptPath);
		}

		public string GetScriptPath(string scriptName)
		{
			throw new NotImplementedException();
		}

		public string GetScriptsDirectory()
		{
			// TODO: Check if it should be possible to configure this somewhere else such as Web.config
			var dir = Path.GetFullPath(
				Environment.CurrentDirectory
				+ "/../../"
				+ "scripts"
				);

			return dir;
		}

		public string GetScriptLink(string scriptPath)
		{
			// TODO: Should this be changed to a "view" page instead?
			return String.Format (
				"Launch.aspx?Script={0}",
				GetScriptName (scriptPath)
			);
		}
	}
}

