//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.IO.Contracts.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll;
//css_ref ../lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll;
//css_ref ../lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using SoftwareMonkeys.csAnt;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.Tests;
using SoftwareMonkeys.csAnt.Tests.Scripting;

class Test_SetUpFromLocalScript : BaseTestScript
{
	public static void Main(string[] args)
	{
		new Test_SetUpFromLocalScript().Start(args);
	}
	
	public override bool Run(string[] args)
	{
	        new FilesGrabber(
                    OriginalDirectory,
                    CurrentDirectory
                    ).GrabOriginalFiles();

                ExecuteScript("Repack-SetUpFromLocal");

                // TODO: Add windows support
	        var originalSetUpFile = OriginalDirectory
                    + Path.DirectorySeparatorChar
                    + "bin"
                    + Path.DirectorySeparatorChar
                    + "Release"
                    + Path.DirectorySeparatorChar
                    + "Packed"
                    + Path.DirectorySeparatorChar
                    + "SetUpFromLocal.exe";

                Console.WriteLine("Set up file original:");
                Console.WriteLine(originalSetUpFile);

                var testProjectDir = Path.GetDirectoryName(CurrentDirectory)
                    + Path.DirectorySeparatorChar
                    + "TestProject";

                var deployedSetUpFile = testProjectDir
                    + Path.DirectorySeparatorChar
                    + Path.GetFileName(originalSetUpFile);

                Console.WriteLine("Setup file new:");
                Console.WriteLine(deployedSetUpFile);
                
                Directory.CreateDirectory(testProjectDir);

                File.Copy(originalSetUpFile, deployedSetUpFile);

                Relocate(testProjectDir);

                var arguments = new string[]{
                    OriginalDirectory,
                    testProjectDir
                    };
                    
                StartDotNetExe(deployedSetUpFile, arguments);

                StartProcess("sh", "csAnt.sh HelloWorld");

                return !IsError;
	}
}
