using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests.Unit
{
	[TestFixture]
	public class FileFinderTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_FindFiles_Star()
		{
			var testScript = GetDummyScript();

			var patterns = new string[]{
				"*"
			};

			var tmpDir = testScript.GetTmpDir();

			var tmpFile = tmpDir
				+ Path.DirectorySeparatorChar
					+ "TestFile.txt";

			File.WriteAllText(tmpFile, "Test content");

			var files = testScript.FindFiles(tmpDir, patterns);

			Assert.AreEqual(1, files.Length, "Wrong number of files.");
		}
		
		[Test]
		public void Test_FindFiles_DoubleStar()
		{
			var testScript = GetDummyScript();

			var patterns = new string[]{
				"**"
			};

			var dir = testScript.CurrentDirectory;

			var tmpFile = dir
				+ Path.DirectorySeparatorChar
				+ "test"
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			Console.WriteLine ("Tmp file: " + tmpFile);
			
			testScript.EnsureDirectoryExists(Path.GetDirectoryName(Path.GetDirectoryName(tmpFile)));
			testScript.EnsureDirectoryExists(Path.GetDirectoryName(tmpFile));

			File.WriteAllText(tmpFile, "Test content");

			// Should get the file even though it's in a sub directory
			var files = testScript.FindFiles(dir, patterns);

			Assert.AreEqual(1, files.Length, "Wrong number of files.");
		}
		
		[Test]
		public void Test_FindFiles_DoubleStarAndExtension()
		{
			var testScript = GetDummyScript();

			var patterns = new string[]{
				"**.txt"
			};

			var dir = testScript.CurrentDirectory;

			var tmpFile = dir
				+ Path.DirectorySeparatorChar
				+ "test"
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			var tmpFile2 = dir
				+ Path.DirectorySeparatorChar
				+ "test"
				+ Path.DirectorySeparatorChar
				+ "TestFile2.log";

			Console.WriteLine ("Tmp file: " + tmpFile);
			
			testScript.EnsureDirectoryExists(Path.GetDirectoryName(Path.GetDirectoryName(tmpFile)));
			testScript.EnsureDirectoryExists(Path.GetDirectoryName(tmpFile));

			File.WriteAllText(tmpFile, "Test content");
			File.WriteAllText(tmpFile2, "Test content");

			// Should get the file even though it's in a sub directory
			var files = testScript.FindFiles(dir, patterns);

			Assert.AreEqual(1, files.Length, "Wrong number of files.");
		}
		
		[Test]
		public void Test_FindFiles_StepUpOneLevel()
		{
			var testScript = GetDummyScript();

			var patterns = new string[]{
				"../*.txt"
			};

			var tmpDir = testScript.GetTmpDir();

			var tmpFile = tmpDir
				+ Path.DirectorySeparatorChar
					+ "TestFile.txt";
			
			var tmpSubDir = tmpDir
				+ Path.DirectorySeparatorChar
				+ "SubDir";

			File.WriteAllText(tmpFile, "Test content");

			var files = testScript.FindFiles(tmpSubDir, patterns);

			Assert.AreEqual(1, files.Length, "Wrong number of files.");
		}

        [Test]
        public void Test_FixPathAndPattern_Normal()
        {
            var path = WorkingDirectory;

            var pattern = "*";

            var fixedPath = path;

            var fixedPattern = pattern;

            var finder = new FileFinder();

            finder.FixPathAndPattern(ref fixedPath, ref fixedPattern);

            Assert.AreEqual (path, fixedPath, "Invalid path.");

            Assert.AreEqual(pattern, fixedPattern, "Invalid pattern.");
        }
        

        [Test]
        public void Test_FixPathAndPattern_StepUp()
        {
            var path = WorkingDirectory;

            var pattern = "../*";

            var fixedPath = path;

            var fixedPattern = pattern;

            var finder = new FileFinder();

            finder.FixPathAndPattern(ref fixedPath, ref fixedPattern);

            var expectedPath = Path.GetDirectoryName(path);

            var expectedPattern = "*";

            Assert.AreEqual (expectedPath, fixedPath, "Invalid path.");

            Assert.AreEqual(expectedPattern, fixedPattern, "Invalid pattern.");
        }
		
		// TODO: Remove if not needed
		/*[Test]
		public void Test_FindFiles_StartsWithSlash()
		{
			var testScript = GetTestScript();

			var patterns = new string[]{
				"/test/*"
			};

			var dir = testScript.CurrentDirectory;

			var tmpFile = dir
				+ Path.DirectorySeparatorChar
				+ "test"
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			Console.WriteLine ("Tmp file: " + tmpFile);

			testScript.EnsureDirectoryExists(Path.GetDirectoryName(tmpFile));

			File.WriteAllText(tmpFile, "Test content");

			var files = testScript.FindFiles(dir, patterns);

			Assert.AreEqual(1, files.Length, "Wrong number of files.");
		}*/
	}
}
