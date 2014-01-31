using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.SetUpFromLocalConsole
{
    public class LocalSetupper
    {
        public LocalSetupper ()
        {
        }

        public void SetUp (string sourceDir, string destinationDir, string[] patternList, bool overwrite)
        {
            if (!Directory.Exists (destinationDir))
                Directory.CreateDirectory (destinationDir);

            // TODO: Inject/construct in constructor
            var fileFinder = new FileFinder();

            var files = fileFinder.FindFiles(sourceDir, patternList);

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

        public string[] GetDefaultFileList()
        {
            return new string[]{
                "lib/csAnt/**",
                "lib/cs-script/**",
                "scripts/*.cs",
                "csAnt.sh"
            };
        }
    }
}

