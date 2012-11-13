using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ListLibs()
		{
			Console.WriteLine ("");
			Console.WriteLine ("The following libraries are currently listed...");
			Console.WriteLine ("");

			var i = 0;

			if (CurrentNode.Nodes.ContainsKey("Libraries"))
			{
				foreach (var node in CurrentNode.Nodes["Libraries"].Nodes.Values)
				{
					i++;

					Console.WriteLine ("");
					Console.WriteLine (i + ": " + node.Name);
					Console.WriteLine ("  " + node.Properties["Url"]);
				}
			}
			else
			{
				Console.WriteLine("No libraries have been listed.");
				Console.WriteLine("No libraries have been listed.");
			}
		}
	}
}

