using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
        // TODO: Move to ProjectNodeState component
		//private FileNode groupNode;
		/// <summary>
		/// Gets information node of the group/company/organization that the project belongs to.
		/// The information comes from the [GroupName].node file in the directory one step above the project directory.
		/// </summary>
		public FileNode GroupNode
		{
			get
			{
                return Nodes.State.GroupNode;
			}
		}
		
		/*public FileNode GetGroupNode()
		{
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting group node...");
				Console.WriteLine("");
			}
			
			// TODO: See if this should be injected via constructor
            var fileNodes = new FileNodeManager(IsVerbose);

			// Get the group directory (one step up from the project directory)
			string dir = Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
			);

			// Scan for the group node
			FileNode node = fileNodes.Get(dir, false, false);
			
			if (node == null)
				throw new GroupNodeNotFoundException();

            if (CurrentNode != null)
                node.Nodes.Add (CurrentNode);
			
			return node;
		}*/
	}
}
