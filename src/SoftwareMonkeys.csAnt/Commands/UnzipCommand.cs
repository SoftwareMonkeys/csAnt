using System;
using SoftwareMonkeys.csAnt.Commands;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class UnzipCommand : BaseScriptCommand
    {
        public string ZipFilePath { get;set; }

        public string DestinationPath { get;set; }

        public string SubPath { get;set; }

        public int TotalFiles { get; set; }

        public UnzipCommand (
            IScript script,
            string zipFilePath,
            string destinationPath,
            string subPath
        ) : base(script)
        {
            ZipFilePath = zipFilePath;
            DestinationPath = destinationPath;
            SubPath = subPath;
        }
        
        public UnzipCommand (
            IScript script,
            string zipFilePath,
            string destinationPath
        ) : base(script)
        {
            ZipFilePath = zipFilePath;
            DestinationPath = destinationPath;
        }

        public override void Execute ()
        {
            
            if (String.IsNullOrEmpty (ZipFilePath))
                throw new ArgumentException ("A zip file path must be provided.", "ZipFilePath");
            
            if (String.IsNullOrEmpty (DestinationPath))
                throw new ArgumentException ("A destination path must be provided.", "DestinationPath");

            Script.EnsureDirectoryExists (DestinationPath);

            // Create a temporary folder name
            var tmpFolder = Script.GetTmpDir ();

            Console.WriteLine ("Unzipping file: " + ZipFilePath);
            Console.WriteLine ("  To: " + DestinationPath);

            ZipInputStream inputStream = null; // used to read from the zip file

            try {
                // create a zip input stream from source zip file
                inputStream = new ZipInputStream (File.OpenRead (ZipFilePath));
 
                // we need to extract to a folder so we must create it if needed
                if (Directory.Exists (tmpFolder) == false)
                    Directory.CreateDirectory (tmpFolder);
 
                ZipEntry entry; // an entry in the zip file which could be a file or directory
 
                // now, walk through the zip file entries and copy each file/directory
                while ((entry = inputStream.GetNextEntry()) != null) {
                    string dirname = Path.GetDirectoryName (entry.Name); // the file path
                    string fname = Path.GetFileName (entry.Name);      // the file name
 
                    // if a path name exists we should create the directory in the destination folder
                    string target = tmpFolder + Path.DirectorySeparatorChar + dirname;
                    if (dirname.Length > 0 && !Directory.Exists (target)) 
                        Directory.CreateDirectory (target);
 
                    // now we know the proper path exists in the destination so copy the file there
                    if (fname != String.Empty) {
                        var filePath = tmpFolder + Path.DirectorySeparatorChar + entry.Name;

                        DecompressAndWriteFile (filePath, inputStream);
                        File.SetLastWriteTime (filePath, entry.DateTime);
                        TotalFiles++;
                    }
                }
            } catch (Exception) {
                throw;
            } finally {
                inputStream.Close ();
            }

            Console.WriteLine ("  Sub path: " + SubPath);

            string fullSubPath = String.Empty;

            if (SubPath.Trim ('/').Trim ('\\') == "*")
                fullSubPath = Directory.GetDirectories (tmpFolder) [0];
            else {
                fullSubPath = Path.GetFullPath (
                    tmpFolder
                    + Path.DirectorySeparatorChar
                    + SubPath
                );
            }
            
            Console.WriteLine ("  Full sub path:");
            Console.WriteLine ("  " + fullSubPath);

            if (fullSubPath.Trim (Path.DirectorySeparatorChar) != DestinationPath.Trim (Path.DirectorySeparatorChar))
            {
                // Move the files in the sub path within the temporary folder into the final destination
                Script.MoveDirectory(fullSubPath, DestinationPath);
            }

            Console.WriteLine ("Extraction complete.");
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

