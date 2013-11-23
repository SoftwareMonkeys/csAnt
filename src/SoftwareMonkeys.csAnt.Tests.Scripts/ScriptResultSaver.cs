using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
	public class TestScriptResultSaver
    {
        public ITestScript Script { get; set; }

        public TestScriptXmlReportFileNamer FileNamer { get; set; }

        public BaseTestFixture TestFixture { get; set; }

		public TestScriptResultSaver (
            ITestScript script,
            TestScriptXmlReportFileNamer fileNamer,
            BaseTestFixture fixture
		)
		{
            Script = script;
            FileNamer = fileNamer;
            TestFixture = fixture;
		}

		public void Save (TestScriptResult result)
        {
            if (String.IsNullOrEmpty(result.ScriptName))
                throw new ArgumentException("The result.ScriptName property is empty.");

            if (String.IsNullOrEmpty(result.Log))
                throw new ArgumentException("The result.Log property is empty.");

            var filePath = GetXmlFilePath();

            // TODO: Re-enable IsVerbose check
            //if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Saving test script result...");
                Console.WriteLine ("Script name:");
                Console.WriteLine (result.ScriptName);
                Console.WriteLine ("Log length:");
                Console.WriteLine (result.Log.Length.ToString());
                Console.WriteLine ("Current dir:");
                Console.WriteLine (result.Script.CurrentDirectory);
                Console.WriteLine ("File:");
                Console.WriteLine (filePath);
                Console.WriteLine ("Success:");
                Console.WriteLine (result.Succeeded);
            //}

			result.Script.EnsureDirectoryExists (Path.GetDirectoryName (filePath));
			

			using (var writer = File.CreateText(filePath))
			{
				var serializer = new XmlSerializer(result.GetType());
				serializer.Serialize(writer, result);
			}

            result.Script.AddSummary("Output test script result to: " + filePath);
		}

        // TODO: Remove function if not needed
        public Stack<string> GetTestScriptStack()
        {
            var detector = new DummyScriptStackDetector(Script);

            return detector.Detect();
        }

        public string GetXmlFilePath()
        {
            return FileNamer.GetXmlFileName(Script, TestFixture);
        }
	}
}

