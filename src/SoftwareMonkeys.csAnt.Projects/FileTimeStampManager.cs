using System;
using System.Collections.Generic;
using System.IO;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.Projects
{
    public class FileTimeStampManager
    {
        public IFileFinder Finder { get;set; }

        public FileTimeStampManager ()
        {
            Finder = new FileFinder();
        }

        public void Update(string projectDirectory)
        {
            var data = GetNewData(projectDirectory);

            WriteNewData(projectDirectory, data);
        }
        
        public Dictionary<string, string> GetPreviousData(string projectDirectory)
        {
            var timeStampsFile = GetTimeStampsFile(projectDirectory);
    
            var timeStamps = new Dictionary<string, string>();
    
            if (File.Exists(timeStampsFile))
            {
                foreach (var line in File.ReadAllLines(timeStampsFile))
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        var parts = line.Split('|');
    
                        var fileName = parts[0];
    
                        var timeStamp = parts[1];
    
                        timeStamps.Add(fileName, timeStamp);
                    }
                }
            }
    
            return timeStamps;
        }

        public Dictionary<string, string> GetNewData(string projectDirectory)
        {
            var patterns = GetPatterns();
        
            var srcDir = projectDirectory
                + Path.DirectorySeparatorChar
                + "src";
        
            var files = Finder.FindFiles(srcDir, patterns);
        
            var timeStamps = new Dictionary<string, string>();
        
            foreach (var file in files)
            {
                var objKey = Path.DirectorySeparatorChar
                    + "obj"
                    + Path.DirectorySeparatorChar;
        
                if (file.IndexOf(objKey) == -1)
                {
                    timeStamps.Add(
                        file.Replace(projectDirectory, ""),
                        File.GetLastWriteTime(file).ToString()
                    );
                }
            }
        
            return timeStamps;
        }
        
        public string[] GetPatterns()
        {
            return new string[]{
                "**.cs",
                "**.csproj",
                "**.sln",
                "!AssemblyInfo.cs",
                "!obj"
            };
        }
        
        public void WriteNewData(string projectDirectory, Dictionary<string, string> timeStampsData)
        {
            var timeStampsFile = GetTimeStampsFile(projectDirectory);
        
            List<string> lines = new List<string>();
        
            foreach (var key in timeStampsData.Keys)
            {
                var timeStamp = timeStampsData[key];
        
                lines.Add(key + "|" + timeStamp);
            }

            Console.WriteLine("");
            Console.WriteLine("Writing out file timestamp data...");
            Console.WriteLine("Lines: " + lines.Count.ToString());
            Console.WriteLine("File: " + timeStampsFile);
            Console.WriteLine("");
        
            File.WriteAllLines(timeStampsFile, lines.ToArray());
        }
        
        public string GetTimeStampsFile(string projectDirectory)
        {
            return projectDirectory
                + Path.DirectorySeparatorChar
                + "src"
                + Path.DirectorySeparatorChar
                + "TimeStamps.txt";
        }
    }
}

