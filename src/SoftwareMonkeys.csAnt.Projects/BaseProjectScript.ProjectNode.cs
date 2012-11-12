using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		private FileNode projectNode;
			
		/// <summary>
		/// Gets/sets the current project information node.
		/// The project information is extracted from the [ProjectName].node file in the root directory of the project.
		/// </summary>
		public FileNode ProjectNode
		{
			get {
				if (projectNode == null)
					projectNode = GetProjectNode();
				return projectNode; }
		}
		
		public FileNode GetProjectNode()
		{
			if (IsVerbose)
			{
				Console.WriteLine("");
				Console.WriteLine("Getting project node...");
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
				Console.WriteLine("Project node directory: " + dir);
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
