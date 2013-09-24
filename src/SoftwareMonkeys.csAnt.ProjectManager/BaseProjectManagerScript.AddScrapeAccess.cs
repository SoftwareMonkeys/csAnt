using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.ProjectManager
{
	public partial class BaseProjectManagerScript
	{
		/// <summary>
		/// Adds a location to access the application by scraping data from a web site.
		/// </summary>
		/// <param name='name'>
		/// Name.
		/// </param>
		/// <param name='scrapeUrl'>
		/// Scrape URL.
		/// </param>
		/// <param name='xPathPattern'>
		/// X path pattern.
		/// </param>
		/// <param name='filterQuery'>
		/// Filter query.
		/// </param>
		public void AddScrapeAccess(
			string name,
			string scrapeUrl,
			string xPathPattern,
			string filterQuery
		)
		{
			// TODO: Remove if not needed. Function should be obsolete.
			EnsureAccessNode();

			var node = NewNode (
				"Access/" + name
			);

			node.Properties["Type"] = "Scrape";

			node.Properties["Url"] = scrapeUrl;

			node.Properties["XPath"] = xPathPattern;

			node.Properties["Filter"] = filterQuery;

			node.Save();
		}
	}
}

