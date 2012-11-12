using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		private FileNode groupNode;
		/// <summary>
		/// Gets information node of the group/company/organization that the project belongs to.
		/// The information comes from the [GroupName].node file in the directory one step above the project directory.
		/// </summary>
		public FileNode GroupNode
		{
			get
			{
				if (groupNode == null)
					groupNode = GetGroupNode();
				return groupNode;
			}
		}
		
		public FileNode GetGroupNode()
		{
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting group node...");
				Console.WriteLine("");
			}
			
			// TODO: See if this should be injected via constructor
			var listener = new ConsoleListener();
			
			// TODO: See if this should be injected via constructor
			var scanner = new FileNodeScanner(
				new FileNodeLoader(
					new FileNodeSaver(),
					listener
				),
				listener
			);

			scanner.Listener.IsVerbose = IsVerbose;

			// Get the group directory (one step up from the project directory)
			string dir = Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
			);

			// Scan for the group node
			FileNode node = scanner.ScanDirectory(dir, false, true);
			
			if (node == null)
				throw new GroupNodeNotFoundException();
			
			return node;
		}
	}
}
