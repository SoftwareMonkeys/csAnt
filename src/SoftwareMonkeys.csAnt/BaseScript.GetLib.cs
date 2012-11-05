using System;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
		public void GetLib(string name)
		{
			GetLib (name, false);
		}

		public void GetLib(string name, bool force)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Retrieving library: " + name);
			Console.WriteLine ("");

			if (ProjectNode.Nodes["Libraries"].Nodes.ContainsKey(name))
			{
				var libNode = ProjectNode.Nodes["Libraries"].Nodes[name];

				// TODO: Use a custom exception
				if (!libNode.Properties.ContainsKey("Url"))
					throw new Exception("No 'Url' property found.");

				string url = libNode.Properties["Url"];
				string subPath = String.Empty;

				if (libNode.Properties.ContainsKey("SubPath"))
					subPath = libNode.Properties["SubPath"];
			
				var libsPath = ProjectDirectory
					+ Path.DirectorySeparatorChar
					+ "lib";

				var libPath = libsPath
					+ Path.DirectorySeparatorChar
					+ name;

				// TODO: Add a date/time stamp in the file name
				var zipFilePath = libPath
					+ Path.DirectorySeparatorChar
					+ name
					+ ".zip";

				DownloadAndUnzip(
					url,
					zipFilePath,
					libPath,
					subPath,
					force
				);
			}
			else
				Console.WriteLine("Error: Library not found: '" + name + "'. Add it using 'AddLib [name] [url]'.");
		}
	}
}

