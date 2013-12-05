using System;
using NUnit.Framework;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class DownloadAndUnzipTestFixture : BaseTestFixture
	{
		public DownloadAndUnzipTestFixture ()
		{
		}

		[Test]
		public void Test_DownloadAndUnzip ()
		{
			var testScript = new DummyScript (
				"TestDownloadAndUnzip"
			);

			testScript.IsVerbose = true;

			var tmpDir = testScript.GetTmpDir ();

			Console.WriteLine ("Tmp dir: " + tmpDir);

			var tmpFile = tmpDir
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			testScript.CurrentDirectory = tmpDir;

			var content = "Hello world";

			using (var writer = File.CreateText(tmpFile)) {
				writer.WriteLine (content);
			}

			var tmpZipFile = tmpDir
				+ Path.DirectorySeparatorChar
				+ "TestZipFile.zip";

			testScript.Zip (tmpZipFile, tmpFile);

			Console.WriteLine ("Test zip file created.");

			var host = "0.0.0.0";

			var port = 8082;
			throw new NotImplementedException(); // This is here because its crashing the whole test batch
			testScript.StartHttp(tmpDir, host, port, false);

			try {
				var url = String.Format (
					"http://{0}:{1}/{2}",
					"localhost", // Use localhost instead of 0.0.0.0
					port,
					Path.GetFileName (tmpZipFile)
				);

				var downloadDir = tmpDir
					+ Path.DirectorySeparatorChar
					+ "Download";

				var downloadFile = downloadDir
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName (tmpZipFile);

				var unzipDir = downloadDir
					+ Path.DirectorySeparatorChar
					+ "Unzip";

				testScript.DownloadAndUnzip(
					url,
					downloadFile,
					unzipDir,
					Path.GetFileNameWithoutExtension(tmpZipFile),
					false
				);

				var unzipFile = unzipDir
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName (tmpFile);

				var unzipFileContent = File.ReadAllText (unzipFile);

				Assert.AreEqual (content.Trim (), unzipFileContent.Trim (), "The unzipped file didn't contain the expected content.");
		
			} catch (Exception ex) {
				Assert.Fail (ex.ToString ());
			} finally {
				//if (process != null)
				//	process.Kill ();
			}
		//}
		}
	}
}

