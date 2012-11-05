using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GetLibs()
		{
			GetLibs (false);
		}

		public void GetLibs(bool force)
		{
			if (!ProjectNode.Nodes.ContainsKey("Libraries"))
				Console.WriteLine ("No libraries listed.");

			foreach (var node in ProjectNode.Nodes["Libraries"].Nodes.Values)
			{
				GetLib (node.Name, force);
			}
		}
	}
}

