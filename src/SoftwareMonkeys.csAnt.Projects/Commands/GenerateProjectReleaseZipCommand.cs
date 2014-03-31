using System;
using SoftwareMonkeys.csAnt.Commands;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class GenerateProjectReleaseZipCommand : BaseProjectScriptCommand
    {
        public string ReleaseName { get;set; }

        public GenerateProjectReleaseZipCommand (BaseProjectScript script, string releaseList) : base(script)
        {
            ReleaseName = releaseList;
        }

        public override void Execute ()
        {
            var rlsDir = GetReleaseDir ();
   
            if (!String.IsNullOrEmpty(ReleaseName))
                CreateRelease (ReleaseName);
            else
            {
                foreach (var listFile in Directory.GetFiles (rlsDir, "*.txt")) {
                    CreateRelease(listFile);
                }    
            }
        }
        
        public void CreateRelease(string listFile)
        {
            if (listFile.IndexOf("-list.txt") == -1)
            {
                listFile = GetReleaseDir()
                    + listFile
                    + "-list.txt";
            }
            
            Console.WriteLine("");
            Console.WriteLine("------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Release list file: " + listFile.Replace(Script.ProjectDirectory, ""));

            var files = new List<string>(File.ReadLines(listFile)).ToArray();       

            if (files.Length > 0)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Patterns:");

                foreach (string file in files)
                {
                    Console.WriteLine("  " + file);
                }

                Console.WriteLine(" ");

                var variation = Path.GetFileNameWithoutExtension(listFile).Replace("-list", "");

                var dateStamp = "[" + Script.TimeStamp + "]";
    
                var version = Script.CurrentNode.Properties.ContainsKey("Version")
                    ? Script.CurrentNode.Properties["Version"]
                    : "0.0.0.0";
                
                var zipFileNameBuilder = new StringBuilder();
                zipFileNameBuilder.Append(Script.ProjectName);
                zipFileNameBuilder.Append("-");
                zipFileNameBuilder.Append(variation);
                zipFileNameBuilder.Append("--");
                zipFileNameBuilder.Append(version.Replace (".", "-"));
                zipFileNameBuilder.Append("-");
                zipFileNameBuilder.Append(dateStamp);
                zipFileNameBuilder.Append(".zip");

                var zipFileName = zipFileNameBuilder.ToString();


                var zipFilePathBuilder = new StringBuilder();

                zipFilePathBuilder.Append(Script.ProjectDirectory);
                zipFilePathBuilder.Append(Path.DirectorySeparatorChar);
                zipFilePathBuilder.Append("rls");
                    zipFilePathBuilder.Append(Path.DirectorySeparatorChar);
                zipFilePathBuilder.Append(variation);
                    zipFilePathBuilder.Append(Path.DirectorySeparatorChar);
                zipFilePathBuilder.Append(zipFileName);

                var zipFilePath = zipFilePathBuilder.ToString();

                if (!Directory.Exists(Path.GetDirectoryName(zipFilePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(zipFilePath));

                Console.WriteLine("Zip file path: " + zipFilePath);

                Script.Zip(
                    zipFilePath,
                    files
                );

                Console.WriteLine("  Release file: " + zipFilePath.Replace(Script.ProjectDirectory, ""));
                Console.WriteLine("Release zip file created successfully.");
                
                Console.WriteLine("");
                Console.WriteLine("------------------------------");
                Console.WriteLine("");

                Script.AddSummary("Generated '" + zipFileName + "' release file from '" + Path.GetFileName(listFile) + "' list file.");

                ExportToGeneralLibs(zipFilePath);
            } 
            else
                Console.WriteLine("No files or patterns specified in the release file list.");

            Console.WriteLine("");
        }

        public void ExportToGeneralLibs(string zipFilePath)
        {
            var generalLibsDir = Path.GetFullPath(
                Script.OriginalDirectory
                + Path.DirectorySeparatorChar
                + ".."
                + Path.DirectorySeparatorChar
                + "lib"
            );

            var generalProjectLibsDir = generalLibsDir
                + Path.DirectorySeparatorChar
                + Script.ProjectName;

            var toFile = generalProjectLibsDir
                + Path.DirectorySeparatorChar
                + Path.GetFileName(zipFilePath);

            Script.EnsureDirectoryExists(generalProjectLibsDir);

            Console.WriteLine("Exporting release file:");
            Console.WriteLine(zipFilePath);
            Console.WriteLine("To:");
            Console.WriteLine(toFile);

            Script.AddSummary("Exported release to: " + Path.GetDirectoryName(toFile));

            File.Copy(zipFilePath, toFile, true);

            // Create a copy without the timestamp (so its easy to know the path to the latest release)

            var fileTimeStamp = "-[" + Script.TimeStamp + "]";

            var modifiedToFile = toFile.Replace(fileTimeStamp, "");
 
            Console.WriteLine("");
            Console.WriteLine("Modified file name:");
            Console.WriteLine(modifiedToFile);
            Console.WriteLine("");
            
            File.Copy(toFile, modifiedToFile, true);
        }

        public string GetReleaseDir()
        {
            return Script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "rls"
                + Path.DirectorySeparatorChar;
        }
    }
}

