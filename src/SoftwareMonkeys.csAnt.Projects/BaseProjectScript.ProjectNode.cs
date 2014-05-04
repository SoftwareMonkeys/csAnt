using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseProjectScript
	{
		private FileNode projectNode;
			
		/// <summary>
		/// Gets/sets the current project information node.
		/// The project information is extracted from the [ProjectName].node file in the root directory of the project.
		/// </summary>
		public FileNode ProjectNode
		{
			get
			{
                return CurrentNode;
			}
		}
	}
}
