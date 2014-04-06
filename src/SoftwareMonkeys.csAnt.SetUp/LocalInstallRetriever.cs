using System;
namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    public class LocaInstallRetriever : BaseInstallerRetriever
    {
        public string[] PatternList { get;set; }

        public bool Overwrite { get;set; }

        public LocaInstallRetriever (
            string[] patternList,
            bool overwrite
            )
        {
            PatternList = patternList;
            Overwrite = overwrite;
            PatternList = GetDefaultFilePatternList();
        }

        public override void Retrieve(string sourceDir, string destinationDir)
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

        public string[] GetDefaultFilePatternList()
        {
            return new string[]{
                "lib/csAnt/**",
                "lib/cs-script/**",
                "lib/FileNodes/**",
                "lib/NUnit/**",
                "lib/NUnitResults/**",
                "lib/HtmlAgilityPack/Net40/**",
                "lib/SharpZipLib/net-20/**",
                "lib/ILRepack.1.25.0/**",
                "scripts/**",
                "csAnt.sh"
            };
        }
    }
}

