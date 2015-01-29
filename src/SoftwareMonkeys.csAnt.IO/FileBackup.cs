using System;
using System.IO;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt
{
    public class FileBackup
    {
        private string workingDirectory;
        public string WorkingDirectory
        {
            get {
                if (String.IsNullOrEmpty(workingDirectory))
                    return Environment.CurrentDirectory;
                return workingDirectory; }
            set { workingDirectory = value; }
        }

        public FileBackup ()
        {
        }

        public string Backup(string relativeFilePath)
        {
        	relativeFilePath = PathConverter.ToRelative(relativeFilePath);
        	
        	Console.WriteLine("");
        	Console.WriteLine("Backing up file:");
        	Console.WriteLine(relativeFilePath);
        	Console.WriteLine("");
        	
            var fromFullFilePath = String.Empty;

            fromFullFilePath = PathConverter.ToAbsolute(relativeFilePath);

            var toFilePath = Environment.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "_bak"
                + Path.DirectorySeparatorChar
                + relativeFilePath;

            var timeStamp = String.Format(
                "[{0}-{1}-{2}--{3}-{4}-{5}]",
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second
            );

            var ext = Path.GetExtension(toFilePath);

            var toFileName = Path.GetFileNameWithoutExtension(toFilePath);

            toFilePath = Path.GetDirectoryName(toFilePath)
                + Path.DirectorySeparatorChar
                + toFileName + ext
                + Path.DirectorySeparatorChar
                + toFileName
                + "-" + timeStamp
                + ext
                + ".bak"; // Add .bak extention to disable certain files which are used based on their extension

            Console.WriteLine("To:");
            Console.WriteLine("  " + PathConverter.ToRelative(toFilePath));
            
            if (!Directory.Exists(Path.GetDirectoryName(toFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(toFilePath));

            File.Copy(
                fromFullFilePath,
                toFilePath
            );

            return toFilePath;
        }
    }
}

