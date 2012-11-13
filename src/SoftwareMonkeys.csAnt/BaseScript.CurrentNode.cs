using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		private FileNode currentNode;
		public FileNode CurrentNode
		{
			get
			{
				if (currentNode == null)
				{
					currentNode = GetCurrentNode();
				}
				return currentNode;
			}
			set { currentNode = value; }
		}
		
		public FileNode GetCurrentNode()
		{
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting current node...");
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

			string dir = CurrentDirectory;
			
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Current node directory: " + dir);
				Console.WriteLine("");
			}
			
			bool foundPropertiesFile = Directory.GetFiles(dir, "*.node").Length > 0;
			
			// Step up the directories looking for .node file
			while (!foundPropertiesFile
			       || dir.IndexOf('/') == dir.LastIndexOf('/'))
			{
				dir = Path.GetDirectoryName(dir);
				
				foundPropertiesFile = Directory.GetFiles(dir, "*.node").Length > 0;
			}

			scanner.Listener.IsVerbose = IsVerbose;
			
			FileNode node = scanner.ScanDirectory(dir, false, true);
			
			return node;
		}
	}
}

