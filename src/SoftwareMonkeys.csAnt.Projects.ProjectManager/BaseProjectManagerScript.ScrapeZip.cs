using System;

namespace SoftwareMonkeys.csAnt.Projects.Access.Scraping
{
	public partial class BaseProjectManagerScript
	{
		public void ScrapeZip(
			string name,
			string scrapeUrl,
			string xPathPattern,
			string filterQuery
		)
		{
			if (!ScrapeExists(name))
			{
				throw new NotImplementedException();
			}

		}
	}
}

