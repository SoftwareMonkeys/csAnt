using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void ImportLocalLib(string projectName, string buildMode)
		{
			ImportLocalLib (GroupName, projectName, buildMode);
		}

		public void ImportLocalLib(string groupName, string projectName, string buildMode)
		{
			if (String.IsNullOrEmpty(groupName))
				groupName = GroupName;

			if (String.IsNullOrEmpty(projectName))
				throw new ArgumentNullException("A project name must be provided.", "projectName");

			if (String.IsNullOrEmpty(buildMode))
				buildMode = "Release";

			// TODO: See if there's a cleaner way to format this path
			string fromDir = Path.GetFullPath(
				ProjectsDirectory
				+ Path.DirectorySeparatorChar
				+ groupName
				+ Path.DirectorySeparatorChar
				+ projectName
				+ Path.DirectorySeparatorChar
				+ "bin"
				+ Path.DirectorySeparatorChar
				+ buildMode
			);
			
			// TODO: See if there's a cleaner way to format this path
			string toDir = Path.GetFullPath(
				ProjectDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ projectName
				+ Path.DirectorySeparatorChar
				+ buildMode
			);
			
			Console.WriteLine ("");
			Console.WriteLine ("Importing local libraries...");
			Console.WriteLine ("");

			Console.WriteLine ("From base directory:");
			Console.WriteLine (fromDir);
			Console.WriteLine ("");

			Console.WriteLine ("Files:");

			// Loop through the files in the source directory
			foreach (string fromFile in Directory.GetFiles(fromDir))
			{
				var toFile = toDir
					+ Path.DirectorySeparatorChar
					+ Path.GetFileName(fromFile);

				Console.WriteLine (
					"  " + fromFile
				);

				// Create the destination diretory if necessary
				if (!Directory.Exists(Path.GetDirectoryName(toFile)))
					Directory.CreateDirectory(Path.GetDirectoryName(toFile));

				// Copy the file from the source location to destination
				File.Copy(fromFile, toFile, true);
			}

			CreateLibNode(
				Path.GetDirectoryName(toDir)
			);
		}

		public void CreateLibNode(string libDir)
		{
			var nodeFileName = Path.GetFileName(libDir);

			var nodeFilePath = libDir
				+ Path.DirectorySeparatorChar
				+ nodeFileName;

			var node = new FileNode(
				nodeFilePath,
				new FileNodeSaver()
			);

			node.Name = "";

			node.Save();

		}
	}
}

