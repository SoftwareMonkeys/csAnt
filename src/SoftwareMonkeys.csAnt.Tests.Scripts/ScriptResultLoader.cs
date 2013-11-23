using System;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
	public class TestScriptResultLoader
	{
		public string ReportsDirectory { get;set; }

		public TestScriptResultLoader (
			string reportsDir
		)
		{
			ReportsDirectory = reportsDir;
		}

		public TestScriptResult Load(
			string xmlResultFile
		)
		{
			TestScriptResult result = null;

			using (var streamReader = File.OpenRead(xmlResultFile))
			{
				var xmlSerializer = new XmlSerializer(typeof(TestScriptResult));

				result = (TestScriptResult)xmlSerializer.Deserialize(streamReader);
			}

			return result;
		}
	}
}

