using System;
using SoftwareMonkeys.csAnt.Tests;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.UI.Web.Tests
{
	[TestFixture]
	public class ScriptUtilitiesTestFixture : BaseTestFixture
	{
		[Test]
		public void Test_GetScriptsDirectory()
		{
			var utilities = GetTestUtilities();

			var dir = utilities.GetScriptsDirectory();

			var expectedDir = Path.GetFullPath(
				Environment.CurrentDirectory
				+ "/../../"
				+ "scripts"
				);

			Assert.AreEqual(expectedDir, dir, "Wrong scripts directory was created.");
		}

		public ScriptUtilities GetTestUtilities()
		{
			return new ScriptUtilities();
		}
	}
}

