using System;
using System.IO;
using System.Web;
using HtmlAgilityPack;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class HtmlReportGenerator
	{
		public string ReportsDirectory { get;set; }

		public ITestScript Script { get; set; }

		public HtmlReportGenerator(
			ITestScript script,
			string reportsDir
		)
		{
			ReportsDirectory = reportsDir;
			Script = script;
		}

		public void Generate ()
		{
			var xmlReportsDir = ReportsDirectory
				+ Path.DirectorySeparatorChar
				+ Script.TimeStamp
				+ Path.DirectorySeparatorChar
				+ "xml";

			foreach (var file in Directory.GetFiles (xmlReportsDir)) {
				GenerateHtmlFromXml(file);
			}
		}

		public void GenerateHtmlFromXml (string file)
		{
			var htmlReportsDir = ReportsDirectory
				+ Path.DirectorySeparatorChar
				+ Script.TimeStamp
				+ Path.DirectorySeparatorChar
				+ "html";

			if (Script.IsVerbose) {
				Console.WriteLine ("Html reports dir:");
				Console.WriteLine (htmlReportsDir);
			}

			var htmlReportFile = htmlReportsDir
				+ Path.DirectorySeparatorChar
				+ Path.GetFileNameWithoutExtension (file)
				+ ".html";

			if (Script.IsVerbose) {
				Console.WriteLine ("Html report file:");
				Console.WriteLine (htmlReportFile);
			}

			var testResultLoader = new TestScriptResultLoader(
				ReportsDirectory
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

		public void UpdateIndex(TestScriptResult result)
		{
			var indexFile = ReportsDirectory
				+ Path.DirectorySeparatorChar
				+ Script.TimeStamp
				+ Path.DirectorySeparatorChar
				+ "html"
				+ Path.DirectorySeparatorChar
				+ "index.html";

			if (!File.Exists(indexFile))
				CreateIndexFile(indexFile);

			var doc = new HtmlDocument();

			doc.Load(indexFile);

			CreateIndexItemNode(doc, result);

			doc.Save(indexFile);
		}

		public HtmlNode CreateIndexItemNode (HtmlDocument doc, TestScriptResult result)
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
			} else {
				itemNode = existingItemNode;
				if (Script.IsVerbose)
					Console.WriteLine ("Existing item '" + name + "' found in index....skipping...");
			}

			return itemNode;
		}

		public void CreateIndexFile(String file)
		{
			var indexTemplate = GetIndexTemplate();

			File.WriteAllText(file, indexTemplate);
		}

		public string GetIndexTemplate ()
		{
			var template = @"<html>
	<head>
		<title>
			Test Script Reports
		</title>
		<LINK href='../../../../../styles/general.css' rel='stylesheet' type='text/css'>
	</head>
	<body>
		<h1>Test Script Reports</h1>
		<table id='ReportsTable' width='100%'>
		</table>
	</body>
</html>";

			return template;
		}

		/*public string GetIndexItemTemplate ()
		{
			var template = @"<tr>
				<td>
					<a href=""{{Name}}.html"">{{Name}}</a>
				</td>
				<td>
					Successful: {{Successful}}
				</td>
			</tr>";

			return template;
		}*/

		public string GetHtmlTemplate(
			string name,
			bool successful,
			string log
		)
		{
			var output = GetHtmlTemplate();

			var statusColor = GetStatusColor(successful);

			output = output.Replace("{{Name}}", name);
			output = output.Replace("{{Successful}}", successful.ToString());
			output = output.Replace("{{Log}}", HttpUtility.HtmlEncode(log).Replace("\n", "<br/>"));
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
		<LINK href='../../../../../styles/general.css' rel='stylesheet' type='text/css'>
	</head>
	<body>
		<h1>{{Name}}</h1>
		<p>Successful: <span style='color:{{StatusColor}}'>{{Successful}}</span></p>
		<p>{{Log}}</p>
	</body>
</html>";

			return template;
		}
	}
}

