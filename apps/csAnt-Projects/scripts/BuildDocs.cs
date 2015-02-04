//css_ref "SoftwareMonkeys.csAnt.dll";
//css_ref "SoftwareMonkeys.csAnt.Projects.dll";

using System;
using System.IO;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.Projects;

class BuildDocs : BaseProjectScript
{
	public static void Main(string[] args)
	{
		new BuildDocs().Start(args);
	}
	
	public override bool Run(string[] args)
	{
		Console.WriteLine("Building doc files...");

        var binDir = ToAbsolute("bin/" + BuildMode);

        var patterns = new string[]{
            GroupName + "." + ProjectName + "*dll",
            GroupName + "." + ProjectName + "*exe"
        };

        var docPath = "doc";

        var htmlPath = docPath + "/html";

        var xmlPath = docPath + "/xml";

        foreach (var file in FindFiles(binDir, patterns))
	    {
            var xmlSubPath = xmlPath + "/" + Path.GetFileNameWithoutExtension(file);
            var htmlSubPath = htmlPath + "/" + Path.GetFileNameWithoutExtension(file);

            // TODO: Add windows support	
            StartProcess(
                "monodocer",
                "-assembly:" + ToRelative(file),
                "-out:" + xmlSubPath
            );

            StartProcess(
                "mdoc",
                "export-html",
                "-o " + htmlSubPath,
                xmlSubPath
            );
        }

		return true;
	}
}
