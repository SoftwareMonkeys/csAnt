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
			/*var cmd = Injection.Retriever.Get<GetLibCommand>(
				new object[]
				{
					this,
					name
				}
			);*/

			var cmd = new GetLibCommand(
				this,
				name
			);


			ExecuteCommand(cmd);

			// TODO: Clean up

		/*	Console.WriteLine ("");
			Console.WriteLine ("Retrieving library: " + name);
			Console.WriteLine ("");

			if (CurrentNode.Nodes == null
			    || !CurrentNode.Nodes.ContainsKey("Libraries"))
			{
				Error ("No libraries listed.");
			}
			else
			{
				if (CurrentNode.Nodes["Libraries"].Nodes.ContainsKey(name))
				{
					var libNode = CurrentNode.Nodes["Libraries"].Nodes[name];

					// TODO: Use a custom exception
					if (!libNode.Properties.ContainsKey("Url"))
						Error("No 'Url' property found.");
					else
					{
						string url = libNode.Properties["Url"];
						string subPath = String.Empty;

						if (libNode.Properties.ContainsKey("SubPath"))
							subPath = libNode.Properties["SubPath"];
					
						var libsPath = CurrentDirectory
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
				}
				else
					Error("Library not found: '" + name + "'. Add it using 'AddLib [name] [url]'.");
			}*/
		}
	}
}

