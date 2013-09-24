using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class DownloadTestFixture
	{
		[Test]
		public void Test_Download()
		{
			throw new NotImplementedException();
			/*var script = new TestScript();

			var url = "http://www.google.com";

			var f = "Test-" + Guid.NewGuid()
				+ Path.DirectorySeparatorChar
				+ "sdf.html";

			var to = System.IO.Path.Combine(
				Environment.GetFolderPath(
					Environment.SpecialFolder.ApplicationData
				),
		    	f
			);

			Directory.CreateDirectory(to);

			var toFile = script.Download(
				url,
				to
			);

			Assert.AreEqual("", toFile);*/
		}
	}
}

