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
				string specifiedDir = BaseNode.Properties["ProjectsDirectory"];

				string projectsDir = String.Empty;

				if (specifiedDir.IndexOf("..") > -1)
				{
					projectsDir = Path.GetDirectoryName(BaseNode.FilePath)
						+ Path.DirectorySeparatorChar
						+ specifiedDir;
				}
				else
					projectsDir = specifiedDir;

				dir = Path.GetFullPath(
					projectsDir
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

