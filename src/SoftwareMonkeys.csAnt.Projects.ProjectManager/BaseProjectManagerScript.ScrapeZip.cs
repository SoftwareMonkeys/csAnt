using System;

namespace SoftwareMonkeys.csAnt.Projects.ProjectManager
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
				AddScrapeAccess(
					name,
					scrapeUrl,
					xPathPattern,
					filterQuery
				);
			}
		}
	}
}

