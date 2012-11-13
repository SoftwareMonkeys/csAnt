using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void RemoveLib(string name)
		{
			RemoveLibNode(name);
		}

		protected void RemoveLibNode(string name)
		{
			if (CurrentNode.Nodes.ContainsKey("Libraries"))
			{
				var libsNode = CurrentNode.Nodes["Libraries"];

				if (libsNode.Nodes.ContainsKey(name))
				{
					var node = libsNode.Nodes[name];

					libsNode.Nodes.Remove(name);

					Console.WriteLine ("Removing library node:");
					Console.WriteLine ("  " + node.FilePath);

					File.Delete (node.FilePath);
				}
			}
		}
	}
}

