using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseProjectScript
	{
		private string projectDirectory = String.Empty;
		/// <summary>
		/// Gets/sets the root directory of the current project.
		/// </summary>
		public string ProjectDirectory
		{
			get
			{
				if (String.IsNullOrEmpty(projectDirectory))
				{
					projectDirectory = Path.GetDirectoryName(ProjectNode.FilePath);

					CurrentDirectory = projectDirectory;
				}
				return projectDirectory;
			}
			set
			{
				projectDirectory = value;
			}
		}
		
	}
}
