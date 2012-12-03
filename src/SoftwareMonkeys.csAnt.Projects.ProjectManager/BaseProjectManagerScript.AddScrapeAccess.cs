using System;
using SoftwareMonkeys.FileNodes;

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
			EnsureAccessNode();

			//var node = new FileNode(

		}
	}
}

