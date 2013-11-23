using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class DownloadTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_Download()
		{
			var testScript = GetDummyScript(true);

			var testFile = testScript.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "TestFile.txt";

			var content = "Hello world";

			File.WriteAllText(testFile, content);

			var host = "0.0.0.0";

			var port = 8082;
			throw new NotImplementedException();
			testScript.StartHttp(testScript.CurrentDirectory, host, port, false);

			// TODO: Remove if not needed
			//testScript.StartNewProcess("http://localhost:8082/TestFile.txt");

			try {
				var url = String.Format (
					"http://{0}:{1}/{2}",
					"localhost", // Use localhost instead of 0.0.0.0
					port,
					Path.GetFileName (testFile)
				);

				var downloadDir = testScript.CurrentDirectory
					+ Path.DirectorySeparatorChar
					+ "Download";

				var downloadFile = downloadDir
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName (testFile);

				testScript.Download(
					url,
					downloadFile
				);
			
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

