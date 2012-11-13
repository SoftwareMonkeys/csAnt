//css_ref ../../lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll;
//css_ref ../../lib/HtmlAgilityPack/Net45/HtmlAgilityPack.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Linq;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Net;
using HtmlAgilityPack;
using System.Collections.Generic;

class PrepareScript
{
	public static void Main(string[] args)
	{
		new PrepareScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		var projectDirectory = GetProjectDirectory();

		Console.WriteLine("Project directory: " + projectDirectory);

		var sourceProjectsDirectory = args[0];

		Console.WriteLine("Source projects dir: " + sourceProjectsDirectory);

		var csAntDir = sourceProjectsDirectory
			+ Path.DirectorySeparatorChar
			+ "SoftwareMonkeys"
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		Console.WriteLine("csAnt dir: " + csAntDir);

		if (Directory.Exists(csAntDir))
			GetLocalcsAnt(csAntDir);
		else
			GetRemotecsAnt();

	}

	public void GetLocalcsAnt(string csAntDir)
	{
		var projectDirectory = GetProjectDirectory();

		var csAntReleaseDir = csAntDir 
			+ Path.DirectorySeparatorChar
			+ "rls"
			+ Path.DirectorySeparatorChar
			+ "ProjectRelease";

		Console.WriteLine("csAnt release dir: " + csAntReleaseDir);

		var csAntReleaseFile = GetNewestFile(csAntReleaseDir);

		Console.WriteLine("csAnt release file: " + csAntReleaseFile);

		var libDir = projectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		var csAntLibDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		var tmpDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt_tmp";

		Console.WriteLine("csAnt lib dir: " + csAntLibDir);

		UnZipFile(csAntReleaseFile, tmpDir);

		var scriptsDir = projectDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		var subDir = Directory.GetDirectories(tmpDir)[0];

		// Move libraries from the tmp directory to the destination
		MoveLibsToDestination(subDir, csAntLibDir);

		// Move scripts from the tmp directory to the destination
		MoveScriptsToDestination(subDir, scriptsDir);

		// Move launcher from the tmp directory to the destination
		MoveLauncherToDestination(subDir, projectDirectory);

		Directory.Delete(tmpDir, true);

	}

	public void GetRemotecsAnt()
	{
		var remotePath = GetRemotecsAntFilePath();

		Console.WriteLine("Remote csAnt path: " + remotePath);

		var projectDirectory = GetProjectDirectory();

		var libDir = projectDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		var csAntLibDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		var tmpDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt_tmp";

		Console.WriteLine("To: " + tmpDir);

		var scriptsDir = projectDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		DownloadAndUnzip(
			remotePath,
			tmpDir
		);

		var subDir = Directory.GetDirectories(tmpDir)[0];

		// Move libraries from the tmp directory to the destination
		MoveLibsToDestination(subDir, csAntLibDir);

		// Move scripts from the tmp directory to the destination
		MoveScriptsToDestination(subDir, scriptsDir);

		// Move launcher from the tmp directory to the destination
		MoveLauncherToDestination(subDir, projectDirectory);

		Directory.Delete(tmpDir, true);
	}

	public string GetRemotecsAntFilePath()
	{
		var url = "https://code.google.com/p/csant/downloads/list";

		var xpath = "//table[@id='resultstable']/tr/td[3]";

		var prefix = "https://csant.googlecode.com/files/";

		var data = ScrapeXPathArray(
			url,
			xpath
		);

		foreach (string item in data)
		{
			if (item.IndexOf("csAnt-release-") == 0)
			{
				return prefix + item;
			}
		}

		return String.Empty;
	}

	public void MoveScriptsToDestination(string tmpDir, string scriptsDir)
	{
		var requiredScriptFiles = new string[]
		{
			"CreateProjectNode.cs",
			"GetLibs.cs"
		};

		if (!Directory.Exists(scriptsDir))
			Directory.CreateDirectory(scriptsDir);

		string fromScriptsDir = tmpDir
			+ Path.DirectorySeparatorChar
			+ "scripts";

		Console.WriteLine("");
		Console.WriteLine("Getting csAnt scripts from:");
		Console.WriteLine(scriptsDir);
		Console.WriteLine("");

		Console.WriteLine("Scripts:");

		foreach (string file in requiredScriptFiles)
		{
			var fromFile = fromScriptsDir
				+ Path.DirectorySeparatorChar
				+ file;

			var toFile = scriptsDir
				+ Path.DirectorySeparatorChar
				+ file;

			Console.WriteLine(toFile);

			if (!File.Exists(toFile))
				File.Copy(fromFile, toFile);
		}

		Console.WriteLine("");

	}

	public void MoveLibsToDestination(string tmpDir, string csAntLibDir)
	{
		if (!Directory.Exists(csAntLibDir))
			Directory.CreateDirectory(csAntLibDir);

		if (Directory.Exists(csAntLibDir))
			Directory.Delete(csAntLibDir, true);

		string libDir = tmpDir
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		Console.WriteLine("");
		Console.WriteLine("Getting csAnt libraries from:");
		Console.WriteLine(libDir);
		Console.WriteLine("");

		Directory.Move(libDir, csAntLibDir);
	}


	public void MoveLauncherToDestination(string tmpDir, string toDir)
	{
		var file = tmpDir
			+ Path.DirectorySeparatorChar
			+ "csAnt.sh";

		var toFile = toDir
			+ Path.DirectorySeparatorChar
			+ "csAnt.sh";

		File.Copy(file, toFile, true);
	}

	public string GetNewestFile(string directory)
	{
		string file = String.Empty;

		var files = new DirectoryInfo(directory).GetFiles().OrderByDescending(p => p.CreationTime);

		foreach (FileInfo f in files)
		{
			file = f.FullName;
			break;
		}

		return file;
	}

    	public static void UnZipFile(string zipFile, string outFolder)
	{
	    ZipFile zf = null;
	    try {
		FileStream fs = File.OpenRead(zipFile);
		zf = new ZipFile(fs);
		foreach (ZipEntry zipEntry in zf) {
		    if (!zipEntry.IsFile) {
		        continue;           // Ignore directories
		    }
		    String entryFileName = zipEntry.Name;
		    // to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
		    // Optionally match entrynames against a selection list here to skip as desired.
		    // The unpacked length is available in the zipEntry.Size property.

		    byte[] buffer = new byte[4096];     // 4K is optimum
		    Stream zipStream = zf.GetInputStream(zipEntry);

		    // Manipulate the output filename here as desired.
		    String fullZipToPath = Path.Combine(outFolder, entryFileName);
		    string directoryName = Path.GetDirectoryName(fullZipToPath);
		    if (directoryName.Length > 0)
		        Directory.CreateDirectory(directoryName);

		    // Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
		    // of the file, but does not waste memory.
		    // The "using" will close the stream even if an exception occurs.
		    using (FileStream streamWriter = File.Create(fullZipToPath)) {
		        StreamUtils.Copy(zipStream, streamWriter, buffer);
		    }
		}
	    } finally {
		if (zf != null) {
		    zf.IsStreamOwner = true; // Makes close also shut the underlying stream
		    zf.Close(); // Ensure we release resources
		}
	    }
	}  

	public string GetProjectDirectory()
	{
		return Path.GetFullPath(".");
	}


	// This function is a copy of the BaseScript.Download function
	public string Download(
		string url,
		string localDestination
	)
	{
		Console.WriteLine ("Downloading...");
		Console.WriteLine ("  From URL: " + url);

		var fileName = Path.GetFileName(url);

		var toFile = localDestination;

		if (localDestination.IndexOf("/") == localDestination.Length-1)
			toFile = localDestination + fileName;
		
		Console.WriteLine ("  To file: " + toFile);

		WebClient webClient = new WebClient();

		webClient.Headers.Add("USER-AGENT", "csAnt");

		webClient.Credentials = CredentialCache.DefaultCredentials;

		Console.WriteLine ("  Please wait...(this may take some time)...");

		webClient.DownloadFile(
			url,
			toFile
		);

		var size = Convert.ToInt32(webClient.ResponseHeaders["Content-Length"]);

		var sizeString = size + "b";

		if (size > 1000*1000)
			sizeString = size / 1000 / 1000 + "mb";
		else if (size > 1000)
			sizeString = size / 1000 + "kb";

		Console.WriteLine ("  Size: " + sizeString);

		Console.WriteLine ("Download complete.");

		return toFile;
	}

	// This function is a copy of the BaseScript.DownloadAndUnzip function
	public void DownloadAndUnzip(string zipFileUrl, string localDirectory)
	{
		// Create a temporary file name
		var tmpFile = GetProjectDirectory()
			+ Path.DirectorySeparatorChar
			+ "_tmp"
			+ Path.DirectorySeparatorChar
			+ "tmp-" + Guid.NewGuid().ToString() + ".zip";

		// Create the _tmp directory if it doesn't exist
		if (!Directory.Exists(Path.GetDirectoryName(tmpFile)))
			Directory.CreateDirectory(Path.GetDirectoryName(tmpFile));

		// Download the zip file to the temporary location
		Download (zipFileUrl, tmpFile);

		// Unzip the zip file
		UnZipFile (tmpFile, localDirectory);

		// Delete the temporary file
		File.Delete(tmpFile);
	}

	public string[] ScrapeXPathArray(
		string url,
		string xpath
	)
	{
		var web = new HtmlWeb();

		var doc = web.Load(url);

		var nodes = doc.DocumentNode.SelectNodes(xpath);

		List<string> values = new List<string>();

		Console.WriteLine("XPath: " + xpath);

		if (nodes != null)
		{
			Console.WriteLine("Total nodes: " + nodes.Count);

			foreach (var node in nodes)
			{
				if (!String.IsNullOrEmpty(node.InnerText.Trim ()))
					values.Add (node.InnerText.Trim ());
			}
		}
		else
		{
			Console.WriteLine("No nodes found.");
		}

		return values.ToArray();
	}
}
