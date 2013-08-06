using System;
using NUnit.Framework;
using System.IO;

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

			var script = new TestScript(this);

			string internalPath = script.GetZipInternalPath(
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

