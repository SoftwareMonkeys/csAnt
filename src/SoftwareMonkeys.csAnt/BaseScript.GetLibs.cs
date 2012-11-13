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
			if (CurrentNode == null)
				Console.WriteLine("CurrentNode wasn't initialized.");

			if (CurrentNode.Nodes == null
			    || !CurrentNode.Nodes.ContainsKey("Libraries"))
			{
				Console.WriteLine ("No libraries listed.");
			}
			else
			{
				foreach (var node in CurrentNode.Nodes["Libraries"].Nodes.Values)
				{
					GetLib (node.Name, force);
				}
			}
		}
	}
}

