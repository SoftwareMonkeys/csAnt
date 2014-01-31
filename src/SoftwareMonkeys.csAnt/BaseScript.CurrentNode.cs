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
                return Nodes.State.CurrentNode;
				/*if (currentNode == null)
				{
					currentNode = GetCurrentNode();
				}
				return currentNode;*/
			}
			set { Nodes.State.CurrentNode = value; }
		}
		
		public FileNode GetCurrentNode()
		{
            throw new Exception("Obsolete");
			/*if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting current node...");
				Console.WriteLine("");
			}

			// TODO: See if this should be injected via constructor
			var fileNodes = new FileNodeManager();

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

			FileNode node = fileNodes.Get (dir, false, true);
			
			return node;*/
		}
	}
}

