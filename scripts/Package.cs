//css_ref ../lib/csAnt/bin/Release/net-40/SoftwareMonkeys.csAnt.External.Nuget.dll

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.External.Nuget;

class PackageScript : BaseScript
{
	public static void Main(string[] args)
	{
		new PackageScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
        var packageName = "";

        if (Arguments.KeylessArguments.Length > 0)
            packageName = Arguments.KeylessArguments[0];
        
        var packer = new NugetPacker();

        if (CurrentNode.Properties.ContainsKey("Status"))
            packer.Status = CurrentNode.Properties["Status"];

        if (CurrentNode.Properties.ContainsKey("Version"))
            packer.Version = new Version(CurrentNode.Properties["Version"]);

        if (String.IsNullOrEmpty(packageName))
            packer.PackAll(CurrentDirectory);
        else
            packer.Pack(CurrentDirectory, packageName);

        RaiseEvent("Package");

		return true;
	}
}
