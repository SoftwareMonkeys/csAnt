//css_ref "SoftwareMonkeys.csAnt.SetUp.dll";

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class SetLibVersion : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new SetLibVersion().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var id = args[0];
        var version = args[1];

        new LibraryVersioner().SetVersion(id, version);

		return !IsError;
	}
}
