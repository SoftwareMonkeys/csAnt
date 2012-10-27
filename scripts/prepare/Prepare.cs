//css_ref ../../lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll;
using System;
using System.IO;
using Microsoft.CSharp;
using System.Diagnostics;
using System.Linq;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;

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
			+ "bin";

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

		// Move from the tmp directory to the destination
		MoveToDestination(tmpDir, csAntLibDir);

		Console.WriteLine("");
		Console.WriteLine("===== Preparation completed successfully! =====");
		Console.WriteLine("");
		Console.WriteLine("You can now run scripts via the csAnt.exe console application. (Pass the script name as the first parameter.)");
		Console.WriteLine("");
		Console.WriteLine("");
	}

	public void GetRemotecsAnt()
	{
		throw new NotImplementedException();
	}

	public void MoveToDestination(string tmpDir, string csAntLibDir)
	{
		if (!Directory.Exists(csAntLibDir))
			Directory.CreateDirectory(csAntLibDir);

		string subDir = Directory.GetDirectories(tmpDir)[0];

		if (Directory.Exists(csAntLibDir))
			Directory.Delete(csAntLibDir, true);

		Directory.Move(subDir, csAntLibDir);

		Directory.Delete(tmpDir);
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
}
