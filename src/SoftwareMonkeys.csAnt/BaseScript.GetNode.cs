using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public FileNode GetNode(string relativePath)
		{
			var parts = relativePath.Replace("\"", "/").Trim('/').Split('/');

			FileNode node = null;

			node = CurrentNode;

			foreach (string part in parts)
			{
				if (node.Nodes.ContainsKey(part))
					node = node.Nodes[part];
			}

			return node;
		}
	}
}

