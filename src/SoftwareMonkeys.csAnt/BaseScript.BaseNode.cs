
using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		private FileNode baseNode;
		/// <summary>
		/// Gets the base information node for the project; such as a Projects.node file in the root directory containing all project group folders, and the project folders within them.
		/// This node contains information related to all projects, in all groups, such as a general backup location, or any other relevant info.
		/// </summary>
		public FileNode BaseNode
		{
			get
			{
				if (baseNode == null)
					baseNode = GetBaseNode();
				return baseNode;
			}
		}
		
		public FileNode GetBaseNode()
		{
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting base node...");
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

			listener.IsVerbose = IsVerbose;
			
			string dir = CurrentDirectory;
			
			FileNode node = scanner.ScanDirectory(dir, true, true);

			return node;
		}
	}
}
