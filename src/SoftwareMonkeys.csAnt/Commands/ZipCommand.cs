using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace SoftwareMonkeys.csAnt
{
    public class ZipCommand : BaseScriptCommand
    {
        public string ZipFilePath { get;set; }
        public string[] FilePatterns { get; set; }

        public ZipCommand (
            IScript script,
            string zipFilePath,
            params string[] filePatterns
        )
            : base(script)
        {
            ZipFilePath = zipFilePath;
            FilePatterns = filePatterns;
        }

        public override void Execute ()
        {
            // This class is obsolete. Use the FileZipper component
            
            // TODO: Fix this function so that files outside the CurrentDirectory can be specified. Currently it causes an error

            Console.WriteLine ("Creating zip file...");

            Console.WriteLine ("  Zip file path: " + ZipFilePath);

            if (!Directory.Exists(Path.GetDirectoryName(ZipFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(ZipFilePath));

            var zipFileName = Path.GetFileNameWithoutExtension(ZipFilePath);

            // Zip up the files - From SharpZipLib Demo Code
            using (ZipOutputStream s = new ZipOutputStream(File.Create(ZipFilePath)))
            {

                if (Script.IsVerbose)
                    Console.WriteLine ("  Current directory: " + Script.CurrentDirectory);
                
                s.SetLevel(1); // 0-9, 9 being the highest compression
    
                byte[] buffer = new byte[4096];
    
                foreach (string patternLine in FilePatterns)
                {
                    if (!String.IsNullOrEmpty(patternLine))
                    {
                        Console.WriteLine ("  Specified pattern/file: " + patternLine);
    
                        string pattern = patternLine;
    
                        if (patternLine.IndexOf("|") > -1)
                        {
                            pattern = patternLine.Split('|')[0];
                        }
    
                        string shortPattern = pattern.Replace(Script.CurrentDirectory, "");
    
                        if (Script.IsVerbose)
                            Console.WriteLine ("  Short pattern: " + shortPattern);
    
                        string[] foundFiles = Script.FindFiles(Script.CurrentDirectory, shortPattern);
                        
                        Console.WriteLine ("    Found " + foundFiles.Length.ToString() + " files.");
                        
                        Console.WriteLine ("    Zipping...");
    
                        foreach (string foundFile in foundFiles)
                        {
                            Console.WriteLine ("    " + foundFile.Replace (Script.CurrentDirectory, ""));
    
                            var internalPath = GetZipInternalPath(
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
        }
        
        public string GetZipInternalPath(string zipName, string fileName, string originalPattern)
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
                subPath = fileName.Replace(Script.CurrentDirectory, "");
            }

            return zipName
                + "/"
                + subPath.Trim('/');
        }
    }
}

