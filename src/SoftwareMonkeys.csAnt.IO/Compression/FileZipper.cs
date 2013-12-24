using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.IO.Compression
{

    public class FileZipper : IFileZipper
    {
        public IFileFinder FileFinder { get;set; }

        public FileZipper (
            IFileFinder fileFinder
        )
        {
            FileFinder = fileFinder;
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

        public void Unzip(string destinationPath)
        {
            throw new NotImplementedException();
        }
    }
}

