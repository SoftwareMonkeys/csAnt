using System;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt.Projects
{
	public partial class BaseProjectScript
	{
		public void ImportLocalWebApp(string projectName, string releaseName)
		{
			ImportLocalWebApp (GroupName, projectName, releaseName);
		}

		public void ImportLocalWebApp(string groupName, string projectName, string releaseName)
		{
			if (String.IsNullOrEmpty(groupName))
				groupName = GroupName;

			if (String.IsNullOrEmpty(projectName))
				throw new ArgumentNullException("A project name must be provided.", "projectName");

			if (String.IsNullOrEmpty(releaseName))
				releaseName = "app";

			// Get the source zip file
			var fromZipFile = GetInstallFile(
				groupName,
				projectName,
				releaseName
			);

			// Get the directory to unzip the zip file to
			string unzipDir = GetUnzipDestination(
				groupName,
				projectName
			);
			
			Console.WriteLine ("");
			Console.WriteLine ("Importing local web application:");
			Console.WriteLine ("");

			// Unzip the zip file
			Unzip (fromZipFile, unzipDir);

			// Get the final destination that the files are to be located
			string newFolderName = GetFinalFolderPath(
				groupName,
				projectName
			);

			Console.WriteLine ("Moving to:");
			Console.WriteLine (newFolderName);
			Console.WriteLine ("");

			// Get the directory containing the files (the directory that was included in the zip file itself)
			string deepUnzipDir = GetDeepUnzipDir(
				groupName,
				projectName
			);

			// Move files from where they were unzipped, to their final destination
			MoveDirectory (deepUnzipDir, newFolderName);

			// Delete the temporary directory
			Directory.Delete (unzipDir);
		}

		public string GetInstallFile(
			string groupName,
			string projectName,
			string releaseName
		)
		{
			// TODO: Check if this path can be more flexible
			// TODO: Check if there's a better way to format this path so it's easier to read (maybe using String.Format(...))
			string fromZipDir = Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ groupName
				+ Path.DirectorySeparatorChar
				+ projectName
				+ Path.DirectorySeparatorChar
				+ "rls"
				+ Path.DirectorySeparatorChar
				+ releaseName
			);

			// Get the newest install file
			string fromZipFile = GetNewestFile(fromZipDir);

			return fromZipFile;
		}

		public string GetUnzipDestination(
			string groupName,
			string projectName
		)
		{
			// TODO: Check if there's a better way to compile this path so it's easier to read (maybe using String.Format(...))
			return Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ projectName
				+ Path.DirectorySeparatorChar
				+ "_tmp"
			);
		}

		public string GetFinalFolderPath(
			string groupName,
			string projectName
		)
		{
			string path = GetUnzipDestination(
				groupName,
				projectName
			);

			path = Path.GetFullPath(
				path
				+ Path.DirectorySeparatorChar
				+ ".."
				+ Path.DirectorySeparatorChar
				+ "app"
			);

			return path;
		}

		public string GetDeepUnzipDir(
			string groupName,
			string projectName
		)
		{
			// Get the unzip location
			string dir = GetUnzipDestination(
				groupName,
				projectName
			);

			// Step into the newest sub directory (the one that was extracted from the zip file)
			string deepDir = GetNewestFolder(dir);

			// Return the path of the sub directory
			return deepDir;

		}
	}
}

