using System;

namespace SoftwareMonkeys.csAnt
{
	/// <summary>
	/// 
	/// </summary>
	public partial class BaseScript
	{
		private string projectName = String.Empty;
		/// <summary>
		/// Gets/sets the current project name.
		/// The name is extracted from the "Name" property of the [ProjectName].node file in the root directory of the project.
		/// </summary>
		public string ProjectName
		{
			get
			{
				if (String.IsNullOrEmpty(projectName))
				{
					projectName = ProjectNode.Name;
					
					Console.WriteLine("");
					Console.WriteLine("Project name: " + projectName);
					Console.WriteLine("");

					if (String.IsNullOrEmpty(projectName))
						throw new Exception("No [ProjectName].node file was found....so the project name cannot be identified.");
				}
				return projectName;
			}
			set
			{
				projectName = value;
			}
		}
	}
}
