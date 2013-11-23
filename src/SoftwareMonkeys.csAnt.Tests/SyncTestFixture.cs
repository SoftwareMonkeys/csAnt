using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class SyncTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Sync()
		{
			var testScript = GetDummyScript();

			testScript.FilesGrabber.GrabOriginalFiles();

			var dir = testScript.CurrentDirectory;

			Console.WriteLine("Tmp dir: " + dir);

			var subDir1 = dir
				+ Path.DirectorySeparatorChar
					+ "One";

			testScript.EnsureDirectoryExists(subDir1);

			var subDir2 = dir
				+ Path.DirectorySeparatorChar
					+ "Two";

			testScript.EnsureDirectoryExists(subDir2);

			var tmpFile = subDir1
				+ Path.DirectorySeparatorChar
					+ "TmpFile.txt";

			var content = "Test content";

			File.WriteAllText(tmpFile, content);

			testScript.Sync(subDir1, subDir2);

			Assert.AreEqual(1, Directory.GetFiles(subDir2).Length, "Wrong number of files found in second directory.");
		}
	}
}

