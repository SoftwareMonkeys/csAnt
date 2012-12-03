using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;
using System.Linq;

namespace SoftwareMonkeys.csAnt
{
	public class GetLibCommand : BaseScriptCommand
	{
		public string LibName { get;set; }

		public bool ForceDownload { get;set; }

		public GetLibCommand (
			IScript script,
			string libName
		)
			: base(script)
		{
			LibName = libName;
		}

		public override void Execute ()
		{
			GetLib(LibName);
		}

		public void GetLib(string name)
		{
			// TODO: See if this function can be simplified and shortened
			Console.WriteLine ("");
			Console.WriteLine ("Retrieving library: " + name);
			Console.WriteLine ("");

			if (Script.CurrentNode.Nodes == null
			    || !Script.CurrentNode.Nodes.ContainsKey("Libraries"))
			{
				Script.Error ("No libraries listed.");
			}
			else
			{
				if (Script.CurrentNode.Nodes["Libraries"].Nodes.ContainsKey(name))
				{
					var libNode = Script.CurrentNode.Nodes["Libraries"].Nodes[name];

					// TODO: Use a custom exception
					if (!libNode.Properties.ContainsKey("Url"))
						Script.Error("No 'Url' property found.");
					else
					{
						bool successful = false;

						// If a local zip file is specified
						if (libNode.Properties.ContainsKey("LocalZipFile"))
						{
							successful = GetLocalZipFileAndExtract(name);
						}

						// If the last attempt failed
						if (!successful)
						{
							// If the zip file URL is specified
							if (libNode.Properties.ContainsKey("Url"))
							{
								successful = DownloadZipAndExtract(name);
							}
						}
					}
				}
				else
					Script.Error("Library not found: '" + name + "'. Add it using 'AddLib [name] [url]'.");
			}
		}

		public bool GetLocalZipFileAndExtract(string name)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Retrieving local zip file for: " + name);
			Console.WriteLine ("");
			
			var libNode = Script.CurrentNode.Nodes["Libraries"].Nodes[name];

			var localZipFile = libNode.Properties["LocalZipFile"];

			var localZipFilePath = GetLatestLocalZipFilePath(localZipFile);

			var zipFilePath = GetZipFilePath(name);

			var destination = GetLibPath(name);

			if (File.Exists(localZipFilePath))
			{
				File.Copy(localZipFilePath, zipFilePath, true);

				Script.Unzip(zipFilePath, destination);

				return true;
			}
			else
				return false;
		}

		public string GetLatestLocalZipFilePath(string localZipFile)
		{
			var output = string.Empty;

			// If there's no wildcard being used
			if (localZipFile.IndexOf("*") == -1)
			{
				output = Path.GetFullPath(localZipFile);
			}
			else
			{
				var dir = Path.GetDirectoryName(localZipFile);
				var file = Path.GetFileName(localZipFile);

				var files = new DirectoryInfo(dir).GetFiles(file).OrderByDescending(p => p.CreationTime)
					.ToArray();

				if (files.Length > 0)
					output = files[0].FullName;

			}

			return output;
		}

		public bool DownloadZipAndExtract(string name)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Downloading zip file for: " + name);
			Console.WriteLine ("");

			var libNode = Script.CurrentNode.Nodes["Libraries"].Nodes[name];

			string url = libNode.Properties["Url"];
			string subPath = String.Empty;

			if (libNode.Properties.ContainsKey("SubPath"))
				subPath = libNode.Properties["SubPath"];

			var zipFilePath = GetZipFilePath(name);

			var libPath = GetLibPath(name);

			Script.DownloadAndUnzip(
				url,
				zipFilePath,
				libPath,
				subPath,
				ForceDownload
			);

			return true;
		}

		public string GetZipFilePath(string name)
		{
			var libPath = GetLibPath(name);

			var zipFilePath = libPath
				+ Path.DirectorySeparatorChar
				+ name
				+ ".zip";

			return zipFilePath;
		}

		public string GetLibPath(string name)
		{
			var libsPath = Script.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib";
			
			// TODO: Add a date/time stamp in the file name
			return libsPath
				+ Path.DirectorySeparatorChar
				+ name;
		}
	}
}

