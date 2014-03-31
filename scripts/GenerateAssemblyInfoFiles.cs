//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Versions.dll;

using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;
using SoftwareMonkeys.csAnt.Versions;

class GenerateAssemblyInfoFilesScript : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new GenerateAssemblyInfoFilesScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("");
		Console.WriteLine("Generating AssemblyInfo files...");
		Console.WriteLine("");

        var srcDir = CurrentDirectory
            + Path.DirectorySeparatorChar
            + "src";

        foreach (var assemblyDir in Directory.GetDirectories(srcDir))
        {
            GenerateIn(assemblyDir);
        }

		return !IsError;
	}

    public void GenerateIn(string assemblyDir)
    {
        var output = GetTemplate();

        var assemblyInfoFilePath = Path.Combine(
            assemblyDir,
            "AssemblyInfo.cs"
        );

        var assemblyTitle = Path.GetFileName(assemblyDir);
        var assemblyCompany = GroupNode.Name;

        var version = new VersionManager().GetVersion(assemblyDir);

        output = output.Replace("{{AssemblyTitle}}", assemblyTitle)
            .Replace("{{AssemblyCompany}}", assemblyCompany)
            .Replace("{{AssemblyVersion}}", version);

        Console.WriteLine("Generating: " + ToRelative(assemblyInfoFilePath));

        File.WriteAllText(assemblyInfoFilePath, output);
    }

    public string GetTemplate()
    {
        return @"using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyTitle(""{{AssemblyTitle}}"")]
[assembly: AssemblyDescription("""")]
[assembly: AssemblyConfiguration("""")]
[assembly: AssemblyCompany(""{{AssemblyCompany}}"")]
[assembly: AssemblyProduct("""")]
[assembly: AssemblyCopyright("""")]
[assembly: AssemblyTrademark("""")]
[assembly: AssemblyCulture("""")]

[assembly: AssemblyVersion(""{{AssemblyVersion}}"")]";
    }
}
