using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public FileNode CurrentNode
		{
			get
			{
                return Nodes.State.CurrentNode;
			}
			set { Nodes.State.CurrentNode = value; }
		}
	}
}

