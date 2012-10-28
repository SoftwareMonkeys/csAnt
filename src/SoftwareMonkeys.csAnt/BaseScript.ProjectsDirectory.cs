using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		private string projectsDirectory = String.Empty;
		/// <summary>
		/// Gets or sets the directory containing all project groups, and the projects within them.
		/// </summary>
		public string ProjectsDirectory
		{
			get
			{
				if (String.IsNullOrEmpty(projectsDirectory))
					projectsDirectory = GetProjectsDirectory();
				return projectsDirectory;
			}
			set { projectsDirectory = value; }
		}

		public string GetProjectsDirectory()
		{
			string dir = String.Empty;

			// If the projects directory is specified in the base node
			if (BaseNode != null && BaseNode.Properties.ContainsKey("ProjectsDirectory"))
			{
				dir = Path.GetFullPath(
					BaseNode.Properties["ProjectsDirectory"]
				);
			}
			// Otherwise just step up above the project directory
			else
			{
				dir = Path.GetFullPath(
					"../.."
				);
			}

			return dir;
		}
	}
}

