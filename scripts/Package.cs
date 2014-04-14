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

        if (args.Length > 0)
            packageName = args[0];
        
        var packer = new NugetPacker();

        if (String.IsNullOrEmpty(packageName))
            packer.PackAll(CurrentDirectory);
        else
            packer.Pack(CurrentDirectory, packageName);

        RaiseEvent("Package");

		return true;
	}

	public void FixName(string name, string scriptFile)
	{
	}
}