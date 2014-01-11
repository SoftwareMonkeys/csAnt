using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.IO.Compression
{

    public class FileZipper : IFileZipper
    {
        public IFileFinder FileFinder { get;set; }

        public DirectoryMover Mover { get;set; }

        public FileZipper (
            IFileFinder fileFinder,
            DirectoryMover mover
        )
        {
            Mover = mover;
            FileFinder = fileFinder;
        }

        public FileZipper ()
        {
            Mover = new DirectoryMover();
            FileFinder = new FileFinder();
        }

        public void Zip(string workingDirectory, string zipFilePath, params string[] filePatterns)
        {
            Console.WriteLine ("Creating zip file...");

            Console.WriteLine ("  Zip file path: " + zipFilePath);

            if (!Directory.Exists(Path.GetDirectoryName(zipFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(zipFilePath));

            var zipFileName = Path.GetFileNameWithoutExtension(zipFilePath);

            // Zip up the files - From SharpZipLib Demo Code
            ZipOutputStream s = new ZipOutputStream(
                File.Create(zipFilePath)
            );

            Console.WriteLine ("  Current directory: " + workingDirectory);
            
            s.SetLevel(9); // 0-9, 9 being the highest compression

            byte[] buffer = new byte[4096];

            foreach (string patternLine in filePatterns)
            {
                if (!String.IsNullOrEmpty(patternLine))
                {
                    Console.WriteLine ("  Specified pattern/file: " + patternLine);

                    string pattern = patternLine;

                    if (patternLine.IndexOf("|") > -1)
                    {
                        pattern = patternLine.Split('|')[0];
                    }

                    string shortPattern = pattern.Replace(workingDirectory, "");

                    Console.WriteLine ("  Short pattern: " + shortPattern);

                    string[] foundFiles = FileFinder.FindFiles(workingDirectory, shortPattern);
                    
                    Console.WriteLine ("    Found " + foundFiles.Length.ToString() + " files.");
                    
                    Console.WriteLine ("    Zipping...");

                    foreach (string foundFile in foundFiles)
                    {
                        Console.WriteLine ("    " + foundFile.Replace (workingDirectory, ""));

                        var internalPath = GetZipInternalPath(
                            workingDirectory,
                            zipFileName,
                            foundFile,
                            patternLine
                        );

                        ZipEntry entry = new ZipEntry(
                            internalPath
                        );

                        entry.DateTime = File.GetLastWriteTime(foundFile);

                        s.PutNextEntry(entry);

                        using (FileStream fs = File.OpenRead(foundFile))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0,
                                buffer.Length);

                               s.Write(buffer, 0, sourceBytes);

                            } while (sourceBytes > 0);
                        }
                    }
                }
            }

            s.Finish();
            s.Close();
        }
        
        public string GetZipInternalPath(string workingDirectory, string zipName, string fileName, string originalPattern)
        {
            var subPath = String.Empty;

            if (originalPattern.IndexOf("|") > -1)
            {
                var patternParts = originalPattern.Split('|');

                subPath = patternParts[1]
                    + Path.GetFileName(fileName);
            }
            else
            {
                subPath = fileName.Replace(workingDirectory, "");
            }

            return zipName
                + "/"
                + subPath.Trim('/');
        }

        public int Unzip(string zipFilePath, string destinationPath, string subPath)
        {
            if (String.IsNullOrEmpty (zipFilePath))
                throw new ArgumentException ("A zip file path must be provided.", "zipFilePath");
            
            if (String.IsNullOrEmpty (destinationPath))
                throw new ArgumentException ("A destination path must be provided.", "destinationPath");

            if (!Directory.Exists (destinationPath))
                Directory.CreateDirectory(destinationPath);

            // Create a temporary folder name
            var tmpFolder = Path.GetDirectoryName(zipFilePath)
                + Path.DirectorySeparatorChar
                    + "_tmpunzip";

            Console.WriteLine ("Unzipping file: " + zipFilePath);
            Console.WriteLine ("  To: " + destinationPath);

            ZipInputStream inputStream = null; // used to read from the zip file

            int totalFiles = 0;

            try {
                // create a zip input stream from source zip file
                inputStream = new ZipInputStream (File.OpenRead (zipFilePath));
 
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
                        totalFiles++;
                    }
                }
            } catch (Exception) {
                throw;
            } finally {
                inputStream.Close ();
            }

            Console.WriteLine ("  Sub path: " + subPath);

            string fullSubPath = String.Empty;

            if (subPath.Trim ('/').Trim ('\\') == "*")
                fullSubPath = Directory.GetDirectories (tmpFolder) [0];
            else {
                fullSubPath = Path.GetFullPath (
                    tmpFolder
                    + Path.DirectorySeparatorChar
                    + subPath
                );
            }
            
            Console.WriteLine ("  Full sub path:");
            Console.WriteLine ("  " + fullSubPath);

            if (fullSubPath.Trim (Path.DirectorySeparatorChar) != destinationPath.Trim (Path.DirectorySeparatorChar))
            {
                // Move the files in the sub path within the temporary folder into the final destination
                Mover.Move(fullSubPath, destinationPath);
            }

            Console.WriteLine ("Extraction complete.");

            return totalFiles;

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

