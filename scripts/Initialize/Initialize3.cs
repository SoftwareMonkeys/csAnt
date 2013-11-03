//css_ref ../../lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll;
//css_ref ../../lib/HtmlAgilityPack/Net40/HtmlAgilityPack.dll;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Xml;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using HtmlAgilityPack;
using System.Collections.Generic;
using Microsoft.CSharp;

/// <summary>
/// Prepares the project for development by ensuring all necessary libraries etc. have been downloaded.
/// </summary>
class PrepareScript
{
	public static void Main(string[] args)
	{
		new PrepareScript().Start(args);
	}
	
	public void Start(string[] args)
	{
		var originalDirectory = GetOriginalDirectory();

		Console.WriteLine("Original directory: " + originalDirectory);

		var sourceProjectsDirectory = args[0];

		sourceProjectsDirectory = Path.GetFullPath(sourceProjectsDirectory);

		Console.WriteLine("Source projects dir: " + sourceProjectsDirectory);

		var currentDirectory = Environment.CurrentDirectory;

		string scriptsDir = currentDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		string generalLibDir = Path.GetFullPath(
			sourceProjectsDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
		);

		Console.WriteLine("");
		Console.WriteLine("General lib dir:");
		Console.WriteLine(generalLibDir);
		Console.WriteLine("");

		string name = "csAnt";

		string csAntLibDir = currentDirectory
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ name;

		string csAntZipInternal = csAntLibDir + "/csAnt.zip";
		string csAntZipLocal = generalLibDir + "/csAnt/csAnt-project-release.zip";
		string csAntZipUrl = GetcsAntUrl();

		GetLib(
			name,
			csAntLibDir,
			csAntZipInternal,
			csAntZipLocal,
			csAntZipUrl,
			generalLibDir
		);


		var subDir = Directory.GetDirectories(csAntLibDir, "csAnt*")[0];

		Console.WriteLine("Sub dir: " + subDir);

		// Move launcher from the tmp directory to the destination
		MoveLauncherToDestination(subDir, originalDirectory);

		// Move libraries from the tmp directory to the destination
		MoveLibsToDestination(subDir, csAntLibDir);

		// Move scripts from the tmp directory to the destination
		MoveScriptsToDestination(subDir, scriptsDir);

		//Directory.Delete(subDir);
	}

	public void UnzipExisting(string csAntZipInternal)
	{
		var originalDirectory = GetOriginalDirectory();

		var scriptsDir = originalDirectory
			+ Path.DirectorySeparatorChar
			+ "scripts";

		var libDir = originalDirectory
			+ Path.DirectorySeparatorChar
			+ "lib";

		var csAntLibDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		var tmpDir = libDir
			+ Path.DirectorySeparatorChar
			+ "csAnt_tmp";

		Unzip(csAntZipInternal, tmpDir);

		var subDir = Directory.GetDirectories(tmpDir)[0];


		// Move launcher from the tmp directory to the destination
		MoveLauncherToDestination(subDir, originalDirectory);
		// Move libraries from the tmp directory to the destination
		MoveLibsToDestination(subDir, csAntLibDir);

		// Move scripts from the tmp directory to the destination
		MoveScriptsToDestination(subDir, scriptsDir);

		//Directory.Delete(tmpDir, true);
	}

	public string GetcsAntUrl()
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
			if (item.IndexOf("csAnt-project-release-") == 0)
			{
				Console.WriteLine("csAnt URL: " + prefix + item);

				return prefix + item;
			}
		}

		return String.Empty;
	}

	public void MoveScriptsToDestination(string tmpDir, string scriptsDir)
	{
		var requiredScriptFiles = new string[]
		{
			"HelloWorld.cs",
			"CreateProjectNode.cs",
			"GetLibs.cs",
			"ImportScript.cs",
			"Update.cs"
		};

		var requiredScriptFolders = new string[]
		{
		};

		if (!Directory.Exists(scriptsDir))
			Directory.CreateDirectory(scriptsDir);

		string fromScriptsDir = tmpDir
			+ Path.DirectorySeparatorChar
			+ "scripts";

		Console.WriteLine("");
		Console.WriteLine("Getting csAnt scripts from:");
		Console.WriteLine(fromScriptsDir);
		Console.WriteLine("");

		Console.WriteLine("Scripts:");

		// Script files
		foreach (string file in requiredScriptFiles)
		{
			var fromFile = fromScriptsDir
				+ Path.DirectorySeparatorChar
				+ file;

			var toFile = scriptsDir
				+ Path.DirectorySeparatorChar
				+ file;

			Console.WriteLine(file);

			File.Copy(fromFile, toFile, true);
		}

		// Script folders
		foreach (string dir in requiredScriptFolders)
		{
			var fromDir = fromScriptsDir
				+ Path.DirectorySeparatorChar
				+ dir;

			var toDir = scriptsDir
				+ Path.DirectorySeparatorChar
				+ dir;

			Console.WriteLine(dir);

			if (!Directory.Exists(toDir))
				Directory.Move(fromDir, toDir);
		}

		Console.WriteLine("");

	}

	public void MoveLibsToDestination(string tmpDir, string csAntLibDir)
	{
		if (!Directory.Exists(csAntLibDir))
			Directory.CreateDirectory(csAntLibDir);

		// TODO: Check if needed
		//if (Directory.Exists(csAntLibDir))
		//	Directory.Delete(csAntLibDir, true);

		string libDir = tmpDir
			+ Path.DirectorySeparatorChar
			+ "lib"
			+ Path.DirectorySeparatorChar
			+ "csAnt";

		Console.WriteLine("");
		Console.WriteLine("Getting csAnt libraries from:");
		Console.WriteLine(libDir);
		Console.WriteLine("");

		MoveDirectory(libDir, csAntLibDir);
	}


	public void MoveLauncherToDestination(string tmpDir, string toDir)
	{
	        var files = new string[]{
	                "csAnt.sh",
	                "csAnt.bat"
	        };
	
	        foreach (var f in files)
	        {
		        var file = tmpDir
			        + Path.DirectorySeparatorChar
			        + f;

		        var toFile = toDir
			        + Path.DirectorySeparatorChar
			        + f;

		        File.Copy(file, toFile, true);
		}
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

    	public static void Unzip(string zipFile, string outFolder)
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

	public string GetOriginalDirectory()
	{
		var path = Path.GetFullPath(".");

		// TODO: Check if there's a more reliable way of doing this (which won't break if tmp paths change)

		if (path.IndexOf(".tmp") > -1)
			path = Path.GetFullPath("../../..");
		else if (path.IndexOf("_tmp") > -1) // TODO: Check if this is needed. The _tmp style shouldn't be used anymore
			path = Path.GetFullPath("../../../..");

		return path;
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

	public void GetLib(
		string name,
		string libDir,
		string zipInternal,
		string zipLocal,
		string zipUrl,
		string generalLibDir
	)
	{
		// TODO: This GetLib function is a duplicate of the python version /GetLib.py.
		// See if it's possible to have this function call the python one, or the other way around, so there's only one GetLib function to maintain

		Console.WriteLine("");
		Console.WriteLine("----------");
		Console.WriteLine("Getting " + name + " library files");
		Console.WriteLine("");

		// Output constants
		Console.WriteLine(name + " Lib Dir:");
		Console.WriteLine(libDir);
		Console.WriteLine("");

		Console.WriteLine(name + " Lib Zip Local:");
		Console.WriteLine(zipLocal);
		Console.WriteLine("");

		Console.WriteLine(name + " Lib Zip Internal:");
		Console.WriteLine(zipInternal);
		Console.WriteLine("");

		Console.WriteLine(name + " Zip URL:");
		Console.WriteLine(zipUrl);
		Console.WriteLine("");

		// If the lib directory doesn't exist then create it
		if (!Directory.Exists(libDir))
			Directory.CreateDirectory(libDir);

		// If the internal css-scrip zip file isn't found try getting it from the local path
		if (!File.Exists(zipInternal))
			GetLibFromLocal(zipLocal, zipInternal);

		// If the internal css-script zip file still isn't found try getting it from the URL
		if (!File.Exists(zipInternal))
			GetLibFromUrl(zipUrl, zipInternal);

		// Extract the files from the internal zip file
		ExtractInternalZipFile( zipInternal, libDir );

		Console.WriteLine(name + " library files have been retrieved.");

		Console.WriteLine("----------");
		Console.WriteLine("");
	}

	public void GetLibFromLocal(
		string zipLocal,
		string zipInternal
	)
	{
		Console.WriteLine("");
		Console.WriteLine("Getting lib from local location...");
		Console.WriteLine("");

		if (File.Exists(zipLocal))
		{
			Console.WriteLine("Copying from:");
			Console.WriteLine(zipLocal);
			Console.WriteLine("To:");
			Console.WriteLine(zipInternal);
			Console.WriteLine("");

			File.Copy(zipLocal, zipInternal);
		}
		else
		{
			Console.WriteLine("Local zip file not found:");
			Console.WriteLine(zipLocal);
			Console.WriteLine("");
		}
	}

	public void GetLibFromUrl(
		string zipUrl,
		string zipInternal
	)
	{
		Console.WriteLine("");
		Console.WriteLine("Getting lib from URL...");
		Console.WriteLine("");

		Console.WriteLine("Downloading from:");
		Console.WriteLine(zipUrl);
		Console.WriteLine("To:");
		Console.WriteLine(zipInternal);
		Console.WriteLine("");

		Download(zipUrl, zipInternal);
	}

	public void ExtractInternalZipFile(
		string zipInternal,
		string libDir
	)
	{
		Console.WriteLine("");
		Console.WriteLine("Extracting files from:");
		Console.WriteLine(zipInternal);
		Console.WriteLine("To:");
		Console.WriteLine(libDir);
		Console.WriteLine("");

		Unzip(zipInternal, libDir);
	}

	public void MoveDirectory(string source, string target)
	{
		Console.WriteLine ("");
		Console.WriteLine ("Moving directory: ");
		Console.WriteLine ("  " + source);
		Console.WriteLine ("To: ");
		Console.WriteLine ("  " + target);
		Console.WriteLine ();

	    var stack = new Stack<Folders>();
	    stack.Push(new Folders(source, target));

	    while (stack.Count > 0)
	    {
	        var folders = stack.Pop();
	        Directory.CreateDirectory(folders.Target);
	        foreach (var file in Directory.GetFiles(folders.Source, "*.*"))
	        {
	             string targetFile = Path.Combine(folders.Target, Path.GetFileName(file));
	             if (File.Exists(targetFile)) File.Delete(targetFile);
	             File.Move(file, targetFile);
	        }

	        foreach (var folder in Directory.GetDirectories(folders.Source))
	        {
	            stack.Push(new Folders(folder, Path.Combine(folders.Target, Path.GetFileName(folder))));
	        }
	    }
	    Directory.Delete(source, true);
	}

	public class Folders
	{
	    public string Source { get; private set; }
	    public string Target { get; private set; }

	    public Folders(string source, string target)
	    {
	        Source = source;
	        Target = target;
	    }
	}
}
