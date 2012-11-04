using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Extracts an array of text from the specified URL, from the InnerContent property of the nodes matching the provided XPath query.
		/// </summary>
		/// <returns>
		/// 
		/// </returns>
		/// <param name='url'>
		/// 
		/// </param>
		/// <param name='xpath'>
		/// 
		/// </param>
		public string[] ScrapeXPathArray(
			string url,
			string xpath
		)
		{
			var web = new HtmlWeb();

			var doc = web.Load(url);

			var nodes = doc.DocumentNode.SelectNodes(xpath);

			List<string> values = new List<string>();

			if (IsVerbose)
			{
				Console.WriteLine("XPath: " + xpath);
			}

			if (nodes != null)
			{
				if (IsVerbose)
					Console.WriteLine("Total nodes: " + nodes.Count);

				foreach (var node in nodes)
				{
					if (!String.IsNullOrEmpty(node.InnerText.Trim ()))
						values.Add (node.InnerText.Trim ());
				}
			}
			else
			{
				if (IsVerbose)
					Console.WriteLine("No nodes found.");
			}

			return values.ToArray();
		}
	}
}

