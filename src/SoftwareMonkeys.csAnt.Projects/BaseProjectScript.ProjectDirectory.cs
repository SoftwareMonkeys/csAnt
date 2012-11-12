using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
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
