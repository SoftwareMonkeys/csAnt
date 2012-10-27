using System;
using System.Collections.Generic;
using System.IO;

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
		
		public void ExecuteScript(string scriptName, string[] args)
		{
			string scriptFile = GetScriptPath(scriptName);
			
			Console.WriteLine("Executing script: " + scriptName);
			
			ExecuteScriptFromFile(scriptFile, args);
		}
		
		public void ExecuteScriptFromFile(string scriptPath, string[] args)
		{			
			// TODO: Check if there's a better way to format this path
			var cscsExe = Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "cs-script"
				+ Path.DirectorySeparatorChar
				+ "cscs.exe"
			);
			
			Console.WriteLine("cscs.exe file: " + cscsExe);
			
			string command = String.Empty;
			
			// If running on mono then the command is "mono"
			if (IsRunningOnMono())
			{
				command = "mono";
			}
			// Otherwise it's the cs-script file
			else
			{
				command = cscsExe;
			}
			
			Console.WriteLine ("Command: " + command);
			
			var argsList = new List<string>();
			
			// If running on mono then the cs-script file path needs to be added as an argument
			if (IsRunningOnMono())
				argsList.Add("\"" + cscsExe + "\"");
			
			argsList.Add("\"" + scriptPath + "\"");
			
			argsList.AddRange(args);
			
			var argsString = String.Join(" ", argsList.ToArray());
			
			Execute(command, argsString);
		}
		
		public string GetScriptPath(string scriptName)
		{
			string scriptPath = string.Empty;
			
			string scriptsDir = GetScriptsPath();
						
			scriptPath = GetStandardScriptPath(scriptsDir, scriptName);
			
			if (String.IsNullOrEmpty(scriptPath))
			{
				Console.WriteLine ("Looking for compiled script.");

				scriptPath = GetCompiledScriptPath(scriptsDir, scriptName);
			}

			if (String.IsNullOrEmpty(scriptPath))
			{
				Console.WriteLine("Can't find script file.");
			}
						
			return scriptPath;
		}

		public string GetStandardScriptPath(string scriptsDir, string scriptName)
		{
			string scriptPath = String.Empty;

			foreach (var p in Directory.GetFiles(scriptsDir))
			{
				string fileName = Path.GetFileNameWithoutExtension(p);

				string ext = Path.GetExtension(p).Trim('.');
				
				if (fileName.ToLower() == scriptName.Trim().ToLower()
				    && ext == "cs")
				{
					scriptPath = p;
				}
				
			}

			return scriptPath;
		}

		public string GetCompiledScriptPath(string scriptsDir, string scriptName)
		{
			string scriptPath = String.Empty;

			foreach (var d in Directory.GetDirectories(scriptsDir))
			{
				var s = Path.GetFileName(d);

				if (s == scriptName)
				{
					string fileName = d
						+ Path.DirectorySeparatorChar
						+ s + ".cs";

					string fileName2 = d
						+ Path.DirectorySeparatorChar
						+ s + "Script.cs";

					if (File.Exists(fileName))
						scriptPath = fileName;

					if (File.Exists(fileName2))
						scriptPath = fileName2;
				}
			}

			return scriptPath;
		}

		public string GetScriptsPath()
		{
			var path = Path.Combine(ProjectDirectory, "scripts");
			
			return path;
			
		}
	}
}
