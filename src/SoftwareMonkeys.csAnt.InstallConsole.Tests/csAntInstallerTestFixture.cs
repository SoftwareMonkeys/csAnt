using System;
using NUnit.Framework;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.InstallConsole.Tests
{
	[TestFixture]
	public class csAntInstallerTestFixture
	{
        [Test]
        public void Test_GetLocalCsAntDir()
        {
            var tmpDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "_tmp"
                    + Path.DirectorySeparatorChar
                    + Guid.NewGuid().ToString();

            Directory.CreateDirectory(tmpDir);

            Environment.CurrentDirectory = tmpDir;

            var installer = new csAntInstaller(
                tmpDir,
                true,
                true
            );

            var csAntDir = installer.GetLocalCsAntDir();

            var expectedDir = Path.GetFullPath(
                tmpDir
                + "/../../../../../csAnt"
            );

            Assert.AreEqual(expectedDir, csAntDir, "Wrong path provided.");

            Directory.Delete (tmpDir, true);
        }
        
        [Test]
        public void Test_GetLocalCsAntDir_NotFound()
        {
            var originalDir = Environment.CurrentDirectory;

            var tmpDir = Path.GetFullPath(
                Environment.CurrentDirectory
                + "../../../../../../_tmp"
            );

            var projectDir = tmpDir
                + Path.DirectorySeparatorChar
                    + "OtherProjects/Group/Project/";

            Directory.CreateDirectory(projectDir);

            Environment.CurrentDirectory = projectDir;

            var installer = new csAntInstaller(
                tmpDir,
                true,
                true
            );

            var csAntDir = installer.GetLocalCsAntDir();

            var expectedDir = "";

            Assert.AreEqual(expectedDir, csAntDir, "Wrong path provided.");
        
            Directory.Delete (tmpDir, true);
        }
        
        [Test]
        public void Test_Install_FromRemote()
        {
            var originalDir = Environment.CurrentDirectory;

            var tmpDir = Path.GetFullPath(
                Environment.CurrentDirectory
                + "../../../../../../_tmp"
            );

            var projectDir = tmpDir
                + Path.DirectorySeparatorChar
                    + "OtherProjects/Group/Project/";

            Directory.CreateDirectory(projectDir);

            Environment.CurrentDirectory = projectDir;

            var installer = new csAntInstaller(
                projectDir,
                true,
                true
            );

            installer.Install("csAnt");
            
            var csAntLibDir = projectDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                    + "csAnt";

            Assert.IsTrue(Directory.Exists(csAntLibDir), "csAnt lib dir not found.");

            var csScriptLibDir = projectDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                    + "cs-script";

            Assert.IsTrue(Directory.Exists(csScriptLibDir), "cs-script lib dir not found.");
            
            var csAntNodeFile = projectDir
                + Path.DirectorySeparatorChar
                + "csAnt.node";

            Assert.IsTrue(File.Exists(csAntNodeFile), "csAnt node file not found.");

            var csAntBashFile = projectDir
                + Path.DirectorySeparatorChar
                + "csAnt.sh";

            Assert.IsTrue(File.Exists(csAntBashFile), "csAnt.sh file not found.");

            var script = new DummyScript("TestScript");

            var cmd = String.Empty;

            if (script.IsLinux)
                cmd = "sh csAnt.sh HelloWorld";
            else
                cmd = "csAnt.bat HelloWorld";

            script.StartProcess(cmd);

            Assert.IsTrue(script.Console.Output.Contains("Hello world!"));

            Assert.IsFalse(script.IsError, "An error occurred.");

            Environment.CurrentDirectory = originalDir;

            Directory.Delete (tmpDir, true);
        }
        
        [Test]
        public void Test_Install_FromLocal()
        {
            var originalDir = Environment.CurrentDirectory;

            var tmpDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "_tmp"
                    + Path.DirectorySeparatorChar
                    + Guid.NewGuid().ToString();

            Directory.CreateDirectory(tmpDir);

            Environment.CurrentDirectory = tmpDir;

            var installer = new csAntInstaller(
                tmpDir,
                true,
                true
            );

            installer.Install("csAnt");
            
            var csAntLibDir = tmpDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                    + "csAnt";

            Assert.IsTrue(Directory.Exists(csAntLibDir), "csAnt lib dir not found.");

            var csScriptLibDir = tmpDir
                + Path.DirectorySeparatorChar
                + "lib"
                + Path.DirectorySeparatorChar
                    + "cs-script";

            Assert.IsTrue(Directory.Exists(csScriptLibDir), "cs-script lib dir not found.");
            
            var csAntNodeFile = tmpDir
                + Path.DirectorySeparatorChar
                + "csAnt.node";

            Assert.IsTrue(File.Exists(csAntNodeFile), "csAnt node file not found.");

            var csAntBashFile = tmpDir
                + Path.DirectorySeparatorChar
                + "csAnt.sh";

            Assert.IsTrue(File.Exists(csAntBashFile), "csAnt.sh file not found.");

            var script = new DummyScript("TestScript");

            var cmd = String.Empty;

            if (script.IsLinux)
                cmd = "sh csAnt.sh HelloWorld";
            else
                cmd = "csAnt.bat HelloWorld";

            script.StartProcess(cmd);

            Assert.IsTrue(script.Console.Output.Contains("Hello world!"));

            Assert.IsFalse(script.IsError, "An error occurred.");

            Environment.CurrentDirectory = originalDir;

            Directory.Delete (tmpDir, true);
        }

		[Test]
		public void Test_InstallFromListFile_FromLocal ()
		{
            var originalDir = Environment.CurrentDirectory;

			var tmpDir = Environment.CurrentDirectory
				+ Path.DirectorySeparatorChar
					+ "_tmp"
					+ Path.DirectorySeparatorChar
					+ Guid.NewGuid().ToString();

            Directory.CreateDirectory(tmpDir);

            Environment.CurrentDirectory = tmpDir;

            var listFile = CreateInstallListFile();

			var installer = new csAntInstaller(
				tmpDir,
				true,
                true
			);

			installer.InstallFromListFile(listFile);
			
			var csAntLibDir = tmpDir
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
					+ "csAnt";

			Assert.IsTrue(Directory.Exists(csAntLibDir), "csAnt lib dir not found.");

			var csScriptLibDir = tmpDir
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
					+ "cs-script";

			Assert.IsTrue(Directory.Exists(csScriptLibDir), "cs-script lib dir not found.");
			
            var csAntNodeFile = tmpDir
                + Path.DirectorySeparatorChar
                + "csAnt.node";

            Assert.IsTrue(File.Exists(csAntNodeFile), "csAnt node file not found.");

            var csAntBashFile = tmpDir
                + Path.DirectorySeparatorChar
                + "csAnt.sh";

            Assert.IsTrue(File.Exists(csAntBashFile), "csAnt.sh file not found.");

            var script = new DummyScript("TestScript");

            var cmd = String.Empty;

            if (script.IsLinux)
                cmd = "sh csAnt.sh HelloWorld";
            else
                cmd = "csAnt.bat HelloWorld";

            script.StartProcess(cmd);

            Assert.IsTrue(script.Console.Output.Contains("Hello world!"));

            Assert.IsFalse(script.IsError, "An error occurred.");
            
            Environment.CurrentDirectory = originalDir;

            Directory.Delete (tmpDir, true);
		}

        [Test]
        public void Test_GetFileList_OneFilePlusOneInclude()
        {
            var originalDir = Environment.CurrentDirectory;

            var tmpDir = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "_tmp"
                    + Path.DirectorySeparatorChar
                    + Guid.NewGuid().ToString();

            var installDir = tmpDir
                + Path.DirectorySeparatorChar
                    + "install";

            Directory.CreateDirectory(installDir);

            var fileOne = installDir
                + Path.DirectorySeparatorChar
                    + "File1.txt";

            var line1 = "Test1.dll, http://somewhere.com/Test1.dll";

            var fileOneContent = new string[]{
                "# Comment", // A comment to be ignored
                line1,
                "" // White space to be ignored
            };

            File.WriteAllLines(fileOne, fileOneContent);
            
            var fileTwo = installDir
                + Path.DirectorySeparatorChar
                    + "File2.txt";

            var line2 = "Test2.dll, http://somewhere.com/Test2.dll";

            var fileTwoContent = new string[]{
                "+File1", // Include reference
                "", // White space to be ignored
                "# Comment", // Comment to be ignored
                line2
            };

            File.WriteAllLines(fileTwo, fileTwoContent);

            var installer = new csAntInstaller(
                tmpDir,
                true,
                true
            );

            var foundLines = installer.GetFileList(fileTwo);

            Assert.AreEqual(2, foundLines.Length, "Wrong number of lines found.");
            
            Assert.AreEqual(line1, foundLines[0], "Line one was incorrect.");
            Assert.AreEqual(line2, foundLines[1], "Line two was incorrect.");
        }

        public string CreateInstallListFile()
        {
            var content = @"
/csAnt.bat, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNFBBdGNtdmw0aUE
/csAnt.node, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qMXJzZ2g0TlBYM0U
/csAnt.sh, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qVWtENHBmSkJuWG8
/lib/csAnt/bin/Release/csAnt.exe, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNzZvZlZqbmxDMDg
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNEo3TUNaRk9nSTA
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qMU9tcUpiclVmNEU
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qakRRWkU2YldOX0k
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Projects.Tests.Scripting.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qZXN5VTdKdS1nSWc
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qWHBQSXdUcDVXRms
/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qSk9qOVZDaXBHRUU
/lib/csAnt/bin/Release/SoftwareMonkeys.FileNodes.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qa2k4eXc5NVpvQW8
/lib/csAnt/bin/Release/CSScriptLibrary.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qZExlZEVYemVjaVk
/lib/cs-script/Lib/Bin/NET 4.0/CSScriptLibrary.dll, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qZExlZEVYemVjaVk
/lib/cs-script/cscs.exe, https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qVnhfY1lMRWU3Zms
/scripts/AddCsAntImport.cs, https://csant.googlecode.com/git/scripts/AddCsAntImport.cs
/scripts/HelloWorld.cs, https://csant.googlecode.com/git/scripts/HelloWorld.cs
/scripts/Update.cs, https://csant.googlecode.com/git/scripts/Update.cs
".Trim ();

            var file = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "install"
                + Path.DirectorySeparatorChar
                    + "test.txt";

            Directory.CreateDirectory(Path.GetDirectoryName(file));

            File.WriteAllText(file, content);

            return file;
        }
	}
}

