using System;

namespace SoftwareMonkeys.csAnt.Projects.Access.Scraping
{
	public partial class BaseProjectManagerScript
	{
		public bool ScrapeExists(string name)

		{
			if (!CurrentNode.Nodes.ContainsKey["Access"])
				return false;

			if (!CurrentNode.Nodes["Access"].Nodes.ContainsKey("ScrapeName"))
				return false;

			return true;



		}
	}
}

