//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";
using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

[ExpectedArgument("name")]
[ExpectedArgument("path")]
class AddImportScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new AddImportScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		if (args.Length < 2)
			Console.WriteLine("Two arguments expected; name and path.");
		else
		{
			string name = args[0];

			string path = args[1];

			Importer.AddImport(name, path);
		}

		return !IsError;
	}
}
