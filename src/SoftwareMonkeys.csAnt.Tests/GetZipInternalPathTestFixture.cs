using System;
using NUnit.Framework;
using System.IO;
using SoftwareMonkeys.csAnt.IO.Compression;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class GetZipInternalPathTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_GetZipInternalPath()
		{
			string fileName = "/SourceDir/File.txt";

			string destination = "/DestDir/";

			string pattern = fileName + "|" + destination;

			string zipFileName = "MyZipFile";

            // TODO: Remove if not ne
			//var script = new DummyScript("TestScript");

            var zipper = new FileZipper();

			string internalPath = zipper.GetZipInternalPath(
                WorkingDirectory,
				zipFileName,
				fileName,
				pattern
			);

			var expected = zipFileName
				+ "/"
				+ destination.Trim('/')
				+ "/"
				+ Path.GetFileName(fileName);

			Assert.AreEqual(expected, internalPath, "Wrong path returned.");
		}
	}
}

