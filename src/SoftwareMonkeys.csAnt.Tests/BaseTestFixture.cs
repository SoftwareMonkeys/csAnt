using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class BaseTestFixture
	{
		public BaseTestFixture ()
		{
		}

		public string GetProjectRoot()
		{
			var path = Path.GetDirectoryName(
				Environment.CurrentDirectory
			);

			path = Path.GetDirectoryName(
				path
			);

			return path;
		}
	}
}

