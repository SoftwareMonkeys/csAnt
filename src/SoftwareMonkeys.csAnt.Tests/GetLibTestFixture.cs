using System;
using NUnit.Framework;
using SoftwareMonkeys.FileNodes;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class GetLibTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_GetLib_ImportScript()
		{
			// TODO: Create a dummy lib file during the test script and check that it's found

			var applicationPath = WorkingDirectory;

			Console.WriteLine ("Application path:");
			Console.WriteLine (applicationPath);

            var script = GetDummyScript("DummyScript");

            new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalScriptingFiles();
            
            var testImportScriptPath = CreateRunTestImportScript(applicationPath);

			script.CurrentDirectory = applicationPath;

			var libNodePath = applicationPath
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ "TestLib"
				+ Path.DirectorySeparatorChar
				+ "TestLib.node";

			var testLibNode = new FileNode(
				libNodePath,
				new FileNodeSaver()
			);

			testLibNode.Name = "TestLib";

			testLibNode.Properties["ImportScript"] = "RunTestImport";
			
			testLibNode.Save();

			script.GetLib("TestLib");

			Directory.Delete(
				Path.GetDirectoryName(
					testLibNode.FilePath
				),
				true
			);

			File.Delete(testImportScriptPath);
		}

		public string CreateRunTestImportScript(string applicationPath)
		{
			var scr = 
@"//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;

class RunTestImportScript : BaseScript
{
	public static void Main(string[] args)
	{
		new RunTestImportScript().Start(args);
	}
	
	public override bool Start(string[] args)
	{
		Console.WriteLine(""Hello world"");

		return true;
	}
}";

			var scrPath = applicationPath
				+ Path.DirectorySeparatorChar
				+ "scripts"
				+ Path.DirectorySeparatorChar
				+ "RunTestImport.cs";

			File.WriteAllText(scrPath, scr);

			return scrPath;
		}
	}
}

