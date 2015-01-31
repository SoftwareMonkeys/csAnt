using System;
using System.IO;
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
            if (Path.GetFileNameWithoutExtension(assemblyDir) != "test-results")
                GenerateIn(assemblyDir);
        }

		return !IsError;
	}

    public void GenerateIn(string assemblyDir)
    {
        Console.WriteLine("Generating in directory:");
        Console.WriteLine(assemblyDir);
    
        var output = GetTemplate();

        var assemblyInfoFilePath = Path.Combine(
            assemblyDir,
            "AssemblyInfo.cs"
        );

        var assemblyTitle = Path.GetFileName(assemblyDir);
        var assemblyCompany = GroupName;

        var version = CurrentNode.Properties.ContainsKey("Version")
            ? CurrentNode.Properties["Version"]
            : "0.0.0.0";

        output = output.Replace("{{AssemblyTitle}}", assemblyTitle)
            .Replace("{{AssemblyCompany}}", assemblyCompany)
            .Replace("{{AssemblyVersion}}", version);

        Console.WriteLine("File: " + ToRelative(assemblyInfoFilePath));

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
