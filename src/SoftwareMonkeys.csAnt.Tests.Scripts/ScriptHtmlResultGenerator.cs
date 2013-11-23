using System;
using System.IO;
using System.Web;
using HtmlAgilityPack;

namespace SoftwareMonkeys.csAnt.Tests.Scripts
{
	public class ScriptHtmlReportGenerator
	{
		public ITestScript Script { get; set; }

        public string XmlResultsDirectory { get;set; }

        public string HtmlResultsDirectory { get;set; }
        
        public ScriptHtmlReportFileNamer HtmlReportFileNamer { get; set; }
        
        public ScriptXmlReportFileNamer XmlReportFileNamer { get; set; }

		public ScriptHtmlReportGenerator(
			ITestScript script,
            ScriptXmlReportFileNamer xmlReportFileNamer,
            ScriptHtmlReportFileNamer htmlReportFileNamer
		)
		{
            HtmlReportFileNamer = htmlReportFileNamer;
            XmlReportFileNamer = xmlReportFileNamer;

			Script = script;
            
            XmlResultsDirectory = xmlReportFileNamer.GetXmlReportsDirectory(script);

            HtmlResultsDirectory = htmlReportFileNamer.GetHtmlReportsDirectory(script);
		}

        public void Generate()
        {
            Generate (XmlResultsDirectory);
        }

        public void Generate (string xmlReportsDir)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Generating test script HTML reports...");
            Console.WriteLine ("");
            Console.WriteLine ("Xml reports:");
            Console.WriteLine (xmlReportsDir);
            Console.WriteLine ("");
            Console.WriteLine ("HTML reports:");
            Console.WriteLine (HtmlResultsDirectory);
            Console.WriteLine ("");
            Console.WriteLine ("Looping through files in folder looking for XML files to convert into HTML files.");

            foreach (var file in Directory.GetFiles (xmlReportsDir)) {
                Console.WriteLine (file);
                GenerateHtmlFromXml (file);
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Looping through subfolders in folder looking for XML files to convert into HTML files.");
            foreach (var dir in Directory.GetDirectories(xmlReportsDir)) {
                Console.WriteLine ("Sub tests dir:");
                Console.WriteLine (dir);

                Generate(dir);
            }
		}

		public void GenerateHtmlFromXml (string file)
		{
            // TODO: Re-enable IsVerbose check
			//if (Script.IsVerbose) {
				Console.WriteLine ("Html reports dir:");
				Console.WriteLine (HtmlResultsDirectory);
			//}

            var htmlReportFile = GetHtmlReportFile(file);
            
            // TODO: Re-enable IsVerbose check
			//if (Script.IsVerbose) {
				Console.WriteLine ("Html report file:");
				Console.WriteLine (htmlReportFile);
			//}

			var testResultLoader = new ScriptResultLoader(
				Script
			);

			var testResult = testResultLoader.Load(file);

			var title = testResult.ScriptName;

			var successful = testResult.Succeeded;

			var log = testResult.Log;

			var output = GetHtmlTemplate(
				title,
				successful,
				log
			);

			Script.EnsureDirectoryExists(Path.GetDirectoryName(htmlReportFile));

			File.WriteAllText(htmlReportFile, output);

			UpdateIndex(testResult);
		}

		public void UpdateIndex(ScriptResult result)
		{
			var indexFile = HtmlResultsDirectory
				+ Path.DirectorySeparatorChar
				+ "index.html";

			if (!File.Exists(indexFile))
				CreateIndexFile(indexFile);

			var doc = new HtmlDocument();

			doc.Load(indexFile);

			CreateIndexItemNode(doc, result);

			doc.Save(indexFile);
		}

		public HtmlNode CreateIndexItemNode (HtmlDocument doc, ScriptResult result)
		{
			return CreateIndexItemNode(doc, result.ScriptName, result.Succeeded, result.Log);
		}
		
		public HtmlNode CreateIndexItemNode (HtmlDocument doc, string name, bool successful, string log)
		{

			var tableElement = doc.DocumentNode.SelectSingleNode ("//table[@id='ReportsTable']");

			if (tableElement == null)
				throw new Exception ("Failed to located TABLE element.");

			var existingItemNode = tableElement.SelectSingleNode ("//tr[@id='" + name + "']");

			HtmlNode itemNode = null;

			if (existingItemNode == null) {
				if (Script.IsVerbose)
					Console.WriteLine ("No existing item '" + name + "' found in index....adding...");

				itemNode = doc.CreateElement ("tr");

				tableElement.AppendChild (itemNode);

				// Name column
				var nameNode = doc.CreateElement ("td");

				itemNode.SetAttributeValue ("id", name);

				itemNode.AppendChild (
					nameNode
				);

				var nameLinkNode = doc.CreateElement ("a");

				var href = name + ".html";

				nameLinkNode.SetAttributeValue ("href", href);

				var nameTextNode = doc.CreateTextNode (name);

				nameLinkNode.AppendChild (nameTextNode);

				nameNode.AppendChild (nameLinkNode);

				// Status column
				var statusNode = doc.CreateElement ("td");

				var statusText = "Succeeded: <span style='color: " + GetStatusColor (successful) + ";'>" + successful.ToString () + "</span>";

				var statusTextNode = doc.CreateTextNode (statusText);

				statusNode.AppendChild (statusTextNode);

				itemNode.AppendChild (statusNode);

				// Sub tests column
				if (SubTestsExist(name))
				{
					var subTestsNode = doc.CreateElement ("td");

					var subTestsText = "<a href='" + name + "/index.html'>Sub Tests</a>";

					var subTestsTextNode = doc.CreateTextNode (subTestsText);

					subTestsNode.AppendChild (subTestsTextNode);

					itemNode.AppendChild (subTestsNode);
				}
			} else {
				itemNode = existingItemNode;
				if (Script.IsVerbose)
					Console.WriteLine ("Existing item '" + name + "' found in index....skipping...");
			}

			return itemNode;
		}

		public bool SubTestsExist (string scriptName)
        {
            var subTestsFolder = HtmlResultsDirectory
                + Path.DirectorySeparatorChar
                + scriptName;


			var doesExist = Directory.Exists(subTestsFolder);
            
            //if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Looking for sub tests...");
                Console.WriteLine ("Script name:");
                Console.WriteLine (scriptName);
                Console.WriteLine ("Sub tests dir:");
                Console.WriteLine (subTestsFolder);
                Console.WriteLine ("Does exist:");
                Console.WriteLine (doesExist.ToString());
                Console.WriteLine ("");
           // }

            return doesExist;
		}

		public void CreateIndexFile(String file)
		{
            Console.WriteLine ("");
            Console.WriteLine ("Creating index file...");
            Console.WriteLine ("Path:");
            Console.WriteLine (file);
            Console.WriteLine ("");

            Script.EnsureDirectoryExists(Path.GetDirectoryName(file));

			var indexTemplate = GetIndexTemplate();

			indexTemplate.Replace("{{ProjectDirectory}}", Script.CurrentDirectory);

			File.WriteAllText(file, indexTemplate);
		}

		public string GetIndexTemplate (string projectDirectory)
		{
			var output = GetIndexTemplate();

			output = output.Replace("{{ProjectDirectory}}", projectDirectory);

			return output;
		}

		public string GetIndexTemplate ()
		{
			var template = @"<html>
	<head>
		<title>
			Test Script Reports
		</title>
		<LINK href='{{ProjectDirectory}}/styles/general.css' rel='stylesheet' type='text/css'>
	</head>
	<body>
		<h1>Test Script Reports</h1>
		<table id='ReportsTable' width='100%'>
		</table>
	</body>
</html>";

			return template;
		}

		public string GetHtmlTemplate(
			string name,
			bool successful,
			string log
		)
		{
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("name", "A name is required.");

            if (String.IsNullOrEmpty(log))
                throw new ArgumentException("log", "The log is required.");

			var output = GetHtmlTemplate();

			var statusColor = GetStatusColor(successful);
			
			output = output.Replace("{{ProjectDirectory}}", Script.CurrentDirectory);
			output = output.Replace("{{Name}}", name);
			output = output.Replace("{{Successful}}", successful.ToString());
			//output = output.Replace("{{Log}}", HttpUtility.HtmlEncode(log).Replace("\n", "<br/>"));
			output = output.Replace("{{Log}}", log.Replace("\n", "<br/>"));
            output = output.Replace("{{StatusColor}}", statusColor);

			return output;
		
		}

		public string GetStatusColor (bool successful)
		{
			var statusColor = "green";
			if (!successful)
				statusColor = "red";

			return statusColor;
		}

		public string GetHtmlTemplate()
		{
			var template = @"<html>
	<head>
		<title>
			{{Name}}
		</title>
		<LINK href='{{ProjectDirectory}}/styles/general.css' rel='stylesheet' type='text/css'>
	</head>
	<body>
		<h1>{{Name}}</h1>
		<p>Successful: <span style='color:{{StatusColor}}'>{{Successful}}</span></p>
		<p>{{Log}}</p>
	</body>
</html>";

			return template;
		}

        public string GetHtmlReportFile(string xmlFile)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Getting HTML report file path for:");
            Console.WriteLine (xmlFile);
            Console.WriteLine ("");

            var htmlFilePath = xmlFile.Replace (XmlResultsDirectory, HtmlResultsDirectory).Replace ("xml", "html");

            Console.WriteLine ("Html file path:");
            Console.WriteLine (htmlFilePath);
            Console.WriteLine ("");

            return htmlFilePath;
        }

        public string GetXmlReportsDir ()
        {
            return ReportsDirectory
                + Path.DirectorySeparatorChar
                + Script.TimeStamp
                + Path.DirectorySeparatorChar
                + "xml";
        }
	}
}

