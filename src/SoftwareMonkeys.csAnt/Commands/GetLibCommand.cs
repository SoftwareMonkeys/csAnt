using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;
using System.Linq;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt
{
	public class GetLibCommand : BaseScriptCommand
	{
		public string LibName { get;set; }

		public bool ForceDownload { get;set; }

		public GetLibCommand (
			IScript script,
			string libName,
			bool force
		)
			: base(script)
		{
			LibName = libName;
			ForceDownload = force;
		}

		public override void Execute ()
		{
			GetLib(LibName);
		}

		public void GetLib (string name)
		{
			// TODO: See if this function can be simplified and shortened
			Console.WriteLine ("");
			Console.WriteLine ("Retrieving library: " + name);
			Console.WriteLine ("");

			if (Script.CurrentNode.Nodes == null
				|| !Script.CurrentNode.Nodes.ContainsKey ("Libraries")) {
				Script.Error ("No libraries listed.");
			} else {
				if (!AlreadyFound (name) || ForceDownload) {
					if (Script.CurrentNode.Nodes ["Libraries"].Nodes.ContainsKey (name)) {
						var libNode = Script.CurrentNode.Nodes ["Libraries"].Nodes [name];

						var successful = false;

						// First: Look for an ImportScript property
						if (libNode.Properties.ContainsKey ("ImportScript")) {
							Console.WriteLine ("");
							Console.WriteLine ("Getting library via import script:");
							Console.WriteLine (libNode.Properties ["ImportScript"]);
							Console.WriteLine ("");

							successful = GetLibByImportScript (libNode);
						}

						// Next (if unsuccessful): Look for a local zip file property
						if (successful == false
							&& libNode.Properties.ContainsKey ("LocalZipFile")) {
							Console.WriteLine ("");
							Console.WriteLine ("Getting library from local zip file:");
							Console.WriteLine (libNode.Properties ["LocalZipFile"]);
							Console.WriteLine ("");

							successful = GetLibByLocal (libNode);
						}

						// Next (if unsuccessful): Look for a URL property
						if (successful == false
							&& libNode.Properties.ContainsKey ("Url")) {
							Console.WriteLine ("");
							Console.WriteLine ("Getting library via URL download:");
							Console.WriteLine ("Url: " + libNode.Properties ["Url"]);
							Console.WriteLine ("Sub path: " + libNode.Properties ["SubPath"]);
							Console.WriteLine ("");

							successful = GetLibByUrl (libNode);
						}

						if (!successful)
							Script.Error ("Couldn't determine import method from '" + libNode.Name + "' library node.");
					} else
						Script.Error ("Library not found: '" + name + "'. Add it using 'AddLib [name] [url]'.");
				}
				else
					Script.Console.WriteLine("'" + name + "' lib is already found. Skipping retrieval.");
			}
		}

		public bool AlreadyFound (string name)
		{
			var libDir = Script.CurrentDirectory
				+ Path.DirectorySeparatorChar
				+ "lib"
				+ Path.DirectorySeparatorChar
				+ name;

			return Directory.GetFiles(libDir).Length > 1
				|| Directory.GetDirectories(libDir).Length > 0;
		}

		public bool GetLocalZipFileAndExtract(string name)
		{
			Console.WriteLine ("");
			Console.WriteLine ("Retrieving local zip file for: " + name);
			Console.WriteLine ("");
			
			var libNode = Script.CurrentNode.Nodes["Libraries"].Nodes[name];

			var localZipFile = libNode.Properties["LocalZipFile"];

			var localZipFilePath = GetLatestLocalZipFilePath(localZipFile);

			var subPath = libNode.Properties["SubPath"];

			var zipFilePath = GetZipFilePath(name);

			var destination = GetLibPath(name);

			if (File.Exists(localZipFilePath))
			{
				File.Copy(localZipFilePath, zipFilePath, true);

				Script.Unzip(zipFilePath, destination, subPath);

				return true;
			}
			else
				return false;
		}

		public string GetLatestLocalZipFilePath(string localZipPath)
		{
			var output = string.Empty;

			// If there's no wildcard being used
			if (localZipPath.IndexOf("*") == -1)
			{
				output = Path.GetFullPath(localZipPath);
			}
			else
			{
				localZipPath = localZipPath.TrimEnd('*');

				localZipPath = Path.GetFullPath(localZipPath);

				Console.WriteLine("Path:");
				Console.WriteLine(localZipPath);

				var dir = localZipPath;

				var files = new DirectoryInfo(dir).GetFiles("*.zip").OrderByDescending(p => p.CreationTime)
					.ToArray();

				if (files.Length > 0)
					output = files[0].FullName;

				Console.WriteLine ("File:");
				Console.WriteLine (output);
				
				Console.WriteLine ("");
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

		public bool GetLibByUrl(FileNode libNode)
		{
			// TODO: Check if this function is needed (and whether the following function can be called directly.

			return DownloadZipAndExtract(libNode.Name);
		}

		public bool GetLibByLocal(FileNode libNode)
		{
			// TODO: Check if this function is needed (and whether the following function can be called directly.

			return GetLocalZipFileAndExtract(libNode.Name);
		}

		public bool GetLibByImportScript(FileNode libNode)
		{
			var scriptName = libNode.Properties["ImportScript"];

			Script.ExecuteScript(scriptName);

			return !Script.IsError;
		}
	}
}

