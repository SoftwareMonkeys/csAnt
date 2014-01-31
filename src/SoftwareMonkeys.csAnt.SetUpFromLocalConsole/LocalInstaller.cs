using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;
using SoftwareMonkeys.csAnt.SetUp.Common;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole
{
    public class LocalInstaller
    {
        public FileFinder Finder { get; set; }

        public LocalInstaller ()
        {
            Finder = new FileFinder();
        }
        
        public void Install (string sourceDir, string destinationDir, string patternListFile, bool overwrite)
        {
            var patternListFilePath = Path.GetFullPath(patternListFile);

            if (!File.Exists (patternListFilePath))
                throw new PatternListFileNotFoundException(patternListFilePath);

            var patterns = File.ReadAllLines(patternListFilePath);

            Install (sourceDir, destinationDir, patterns, overwrite); 
        }

        public void Install (string sourceDir, string destinationDir, string[] patternList, bool overwrite)
        {
            if (!Directory.Exists (destinationDir))
                Directory.CreateDirectory (destinationDir);

            var files = Finder.FindFiles(sourceDir, patternList);

            Console.WriteLine ("Total files: " + files.Length);

            foreach (var file in files) {
                var toFile = file.Replace (sourceDir, destinationDir);
            
                if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                    Directory.CreateDirectory (Path.GetDirectoryName (toFile));

                Console.WriteLine ("  Copying:");
                Console.WriteLine ("    " + file);
                Console.WriteLine ("  To:");
                Console.WriteLine ("    " + toFile);

                if (overwrite || !File.Exists (toFile))
                    File.Copy (file, toFile);
                else
                    Console.WriteLine ("  Skipping copy.");
            }
        }
    }
}

