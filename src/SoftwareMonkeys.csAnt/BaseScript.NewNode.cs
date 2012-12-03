using System;
using SoftwareMonkeys.FileNodes;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		/// <summary>
		/// Creates a new file node at the specified relative path.
		/// </summary>
		/// <returns>
		/// The node.
		/// </returns>
		/// <param name='relativePath'>
		/// A path relative to the current directory such as '/Info/MyFiles/".
		/// </param>
		public FileNode NewNode(string relativePath)
		{
			EnsureParentNodes(relativePath);

			var name = Path.GetFileName(relativePath);

			var path = Path.GetFullPath(
				CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ relativePath
				+ Path.DirectorySeparatorChar
				+ name
				+ ".node"
			);
			
			Console.WriteLine ("");
			Console.WriteLine ("Creating node:");
			Console.WriteLine (path);
			Console.WriteLine ("");

			var node = new FileNode(
				path,
				new FileNodeSaver()
			);

			node.Name = name;

			return node;
		}
	}
}

