using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

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
            Install (sourceDir, destinationDir, File.ReadAllLines(Path.GetFullPath(patternListFile)), overwrite); 
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
