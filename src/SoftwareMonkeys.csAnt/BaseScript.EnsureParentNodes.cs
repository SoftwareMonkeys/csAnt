using System;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void EnsureParentNodes(string relativePath)
		{
			var node = GetNode (relativePath);

			if (node == null)
			{
				NewNode(relativePath);
			}
		}

	}
}

