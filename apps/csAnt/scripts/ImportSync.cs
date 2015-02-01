//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class ImportSyncScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new ImportSyncScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var projectName = args[0];

        Importer.Sync(projectName);
                
		return !IsError;
	}
}
