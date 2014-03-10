using System;
using System.IO;


namespace SoftwareMonkeys.csAnt.IO
{
    public class FileSync
    {
        public IFileFinder Finder { get;set; }
        public FileBackup FileBackup { get;set; }

        public FileSync ()
        {
            Finder = new FileFinder();
            FileBackup = new FileBackup();
        }

        public void Sync(string fromDir, string toDir, params string[] patterns)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Synchronising files...");
            Console.WriteLine ("");
            
            Console.WriteLine("From:");
            Console.WriteLine(fromDir);

            Console.WriteLine("To:");
            Console.WriteLine(toDir);
            Console.WriteLine ("");

            var totalSynced = 0;
            var totalSkipped = 0;
            var totalDeleted = 0;

            foreach (var file in Finder.FindFiles (fromDir, patterns)) {
                var toFile = file.Replace(fromDir, toDir);

                IOUtility.EnsureDirectoryExists(Path.GetDirectoryName(toFile));

                if (!File.Exists(toFile))
                {
                    Console.WriteLine (toFile.Replace(toDir, ""));

                    File.Copy(file, toFile);

                    totalSynced ++;
                }
                else if (File.GetLastWriteTime(file) > File.GetLastWriteTime(toFile))
                {
                    Console.WriteLine (toFile.Replace(toDir, ""));

                    FileBackup.Backup(toFile);

                    File.Copy(file, toFile, true);

                    totalSynced ++;
                }
                else
                {
                    Console.WriteLine ("Skipping: " + toFile.Replace(toDir, ""));

                    totalSkipped ++;
                }
            }
            
            Console.WriteLine ("");
            Console.WriteLine ("Total synced: " + totalSynced);
            Console.WriteLine ("Total skipped: " + totalSkipped);
            Console.WriteLine ("Total deleted: " + totalDeleted);
            Console.WriteLine ("");
            Console.WriteLine ("Sync finished!");

            // TODO: Add the ability to delete obsolete files
        }
    }
}

