using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class ClearTmpTestFixture : BaseUnitTestFixture
	{
		[Test]
		public void Test_ClearTmp()
		{
			var testScript = new DummyScript(
				"ClearTmpTest"
			);

			var tmpRoot = testScript.GetTmpRoot();

			Console.WriteLine("Tmp root: " + tmpRoot);

			var tmpFile = testScript.GetTmpFile();

			Console.WriteLine("Tmp file: " + tmpFile);

			var tmpDir = Path.GetDirectoryName(tmpFile);

			Console.Write("Tmp dir: " + tmpDir);

			if (!Directory.Exists(tmpDir))
				Directory.CreateDirectory(tmpDir);

			using (var writer = File.CreateText(tmpFile))
				writer.Write ("Hello world");

			Assert.IsTrue(Directory.Exists(tmpRoot), "The temporary directory root path doesn't exist.");

			testScript.ClearTmp();

			Assert.IsFalse(Directory.Exists(tmpRoot), "The temporary directory root path still exists when it should have been cleared.");
		}
	}
}

