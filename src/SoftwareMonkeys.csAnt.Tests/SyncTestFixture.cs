using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class SyncTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Sync()
		{
			var script = GetDummyScript();

			new FilesGrabber(
                script.OriginalDirectory,
                script.CurrentDirectory
                ).GrabOriginalFiles();

			var dir = script.CurrentDirectory;

			Console.WriteLine("Tmp dir: " + dir);

			var subDir1 = dir
				+ Path.DirectorySeparatorChar
					+ "One";

			script.EnsureDirectoryExists(subDir1);

			var subDir2 = dir
				+ Path.DirectorySeparatorChar
					+ "Two";

			script.EnsureDirectoryExists(subDir2);

			var tmpFile = subDir1
				+ Path.DirectorySeparatorChar
					+ "TmpFile.txt";

			var content = "Test content";

			File.WriteAllText(tmpFile, content);

			script.Sync(subDir1, subDir2);

			Assert.AreEqual(1, Directory.GetFiles(subDir2).Length, "Wrong number of files found in second directory.");
		}
	}
}

