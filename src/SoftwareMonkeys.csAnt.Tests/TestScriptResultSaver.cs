using System;
using System.IO;
using System.Xml.Serialization;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestScriptResultSaver
	{
		public string Directory = String.Empty;

		public TestScriptResultSaver (
			string dir
		)
		{
			Directory = dir;
		}

		public void Save (TestScriptResult result)
		{
			var filePath = Directory
				+ Path.DirectorySeparatorChar
				+ result.Script.TimeStamp
				+ Path.DirectorySeparatorChar
				+ "xml"
				+ Path.DirectorySeparatorChar
				+ result.ScriptName
				+ ".xml";

			result.Script.EnsureDirectoryExists (Path.GetDirectoryName (filePath));
			
			if (result.Script.IsVerbose) {
				Console.WriteLine ("Result output:");
				Console.WriteLine (filePath);
			}

			using (var writer = File.CreateText(filePath))
			{
				var serializer = new XmlSerializer(result.GetType());
				serializer.Serialize(writer, result);
			}
		}
	}
}

