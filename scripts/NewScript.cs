//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class NewScriptScript : BaseScript
{
	public static void Main(string[] args)
	{
		new NewScriptScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		var name = args[0];

		Console.WriteLine("Creating new script...");
		Console.WriteLine("Name: " + name);

		var sourceScript = GetScriptPath("HelloWorld");
		sourceScript = Path.GetDirectoryName(sourceScript)
			+ Path.DirectorySeparatorChar
			+ "Template.template";

		var destScript = Path.GetDirectoryName(sourceScript)
			+ Path.DirectorySeparatorChar
			+ name
			+ ".cs";

		if (File.Exists(destScript))
			Console.WriteLine("A script with that name already exists. Cancelled.");
		else
		{
			File.Copy(sourceScript, destScript);

			FixName(name, destScript);

			StartNewProcess(destScript);

			Console.WriteLine("Finished.");
		}

		return true;
	}

	public void FixName(string name, string scriptFile)
	{
		var content = File.ReadAllText(scriptFile);

		content = content.Replace("Template", name);

		File.WriteAllText(scriptFile, content);
	}
}
