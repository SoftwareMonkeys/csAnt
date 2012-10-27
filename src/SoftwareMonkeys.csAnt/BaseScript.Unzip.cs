using System;
using System.IO;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;

namespace SoftwareMonkeys.csAnt
{
	public partial class BaseScript
	{
	    public int Unzip(string zipFilePath, string destinationPath)
	    {
			Console.WriteLine("Unzipping file: " + zipFilePath);
			Console.WriteLine("To: " + destinationPath);

			ZipInputStream zinstream = null; // used to read from the zip file
            int numFileUnzipped = 0; // number of files extracted from the zip file
 
            try
            {
                // create a zip input stream from source zip file
                zinstream = new ZipInputStream(File.OpenRead(zipFilePath));
 
                // we need to extract to a folder so we must create it if needed
                if (Directory.Exists(destinationPath) == false)
                    Directory.CreateDirectory(destinationPath);
 
                ZipEntry theEntry; // an entry in the zip file which could be a file or directory
 
                // now, walk through the zip file entries and copy each file/directory
                while ((theEntry = zinstream.GetNextEntry()) != null)
                {
                    string dirname = Path.GetDirectoryName(theEntry.Name); // the file path
                    string fname   = Path.GetFileName(theEntry.Name);      // the file name
 
                    // if a path name exists we should create the directory in the destination folder
                    string target = destinationPath + Path.DirectorySeparatorChar + dirname;
                    if (dirname.Length > 0 && !Directory.Exists(target)) 
                        Directory.CreateDirectory(target);
 
                    // now we know the proper path exists in the destination so copy the file there
                    if (fname != String.Empty)
                    {
                        DecompressAndWriteFile(destinationPath + Path.DirectorySeparatorChar + theEntry.Name, zinstream);
                        numFileUnzipped++;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                zinstream.Close();
            }

			Console.WriteLine ("Extraction complete.");
 
            return numFileUnzipped;
	    }

		private static void DecompressAndWriteFile( string destination, ZipInputStream source )
        {
            FileStream wstream = null;
 
            try
            {
                // create a stream to write the file to
                wstream = File.Create(destination);
 
                const int block = 2048; // number of bytes to decompress for each read from the source
 
                byte[] data = new byte[block]; // location to decompress the file to
 
                // now decompress and write each block of data for the zip file entry
                while (true)
                {
                    int size = source.Read(data, 0, data.Length);
 
                    if (size > 0)
                        wstream.Write(data, 0, size);
                    else
                        break; // no more data
                }
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                if( wstream != null )
                    wstream.Close();
            }
        }
	}
}

