using System;
using System.IO;
using SoftwareMonkeys.csAnt.SourceControl.Git;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt.IO
{
    public class TextReplacer
    {
        public IFileFinder Finder { get;set; }

        public Gitter Git { get;set; }

        public string[] ExcludePatterns = new string[]{
            "!.git",
            "!logs/",
            "!tests/results/",
            "!profiling/"
        };

        public TextReplacer ()
        {
            Finder = new FileFinder();
        }

        public void ReplaceIn(string workingDir, string filePattern, string textToFind, string replacementText, bool commit)
        {
            ReplaceIn(workingDir, new string[]{filePattern}, textToFind, replacementText, commit);
        }

        public void ReplaceIn(string workingDir, string[] filePatterns, string textToFind, string replacementText, bool commit)
        {
            var patterns = new List<string>();
            patterns.AddRange(filePatterns);
            patterns.AddRange(ExcludePatterns);

            string[] files = Finder.FindFiles(workingDir, patterns.ToArray());

            Console.WriteLine("Performing (" + (commit ? "real" : "mock") + ") replace of the following text...");
            Console.WriteLine("From:");
            Console.WriteLine("  " + textToFind);
            Console.WriteLine("To:");
            Console.WriteLine("  " + replacementText);

            var totalChanges = 0;
            var totalSkipped = 0;
            
            foreach (string file in files)
            {
                if (IsSupportedFile(file))
                {
                    string content = String.Empty;

                    try
                    {
                        content = OpenFile(file);
                    }
                    catch
                    {
                        Console.WriteLine("Can't load: " + file);
                    }

                    // Check file text
                    if (!String.IsNullOrEmpty(content)
                        && content.Contains(textToFind))
                    {
                        Console.WriteLine("Text found in file: " + PathConverter.ToRelative(file));
                        if (commit)
                        {
                            content = content.Replace(textToFind, replacementText);
                            SaveFile(file, content);
                            Console.WriteLine("Text replaced. File updated.");
                            totalChanges++;
                        }
                        else
                            totalSkipped++;
                    }
                }
                
                // Check file name
                if (file.Contains(textToFind))
                {
                    Console.WriteLine("Text found in file name: " + PathConverter.ToRelative(file));
                    if (commit)
                    {
                        var newFileName = file.Replace(textToFind, replacementText);

                        DirectoryChecker.EnsureDirectoryExists(Path.GetDirectoryName(newFileName));

                        try
                        {
                            Git.Move(file, newFileName);
                            Console.WriteLine("Renamed (git move)");
                        }
                        catch
                        {
                            File.Move(file, newFileName);
                            Console.WriteLine("Renamed (direct file move/rename)");
                        }
                        totalChanges++;
                    }
                    else
                        totalSkipped++;
                }
            }
            
            Console.WriteLine("");
            Console.WriteLine("Total changes: " + totalChanges);
            Console.WriteLine("Total skipped: " + totalSkipped);
            Console.WriteLine("");
            if (!commit)
                Console.WriteLine("The replacement wasn't committed.");
            Console.WriteLine("");
            Console.WriteLine("Replacement complete!");
            Console.WriteLine("");
        }
        
        public string OpenFile(string filePath)
        {
            string content = String.Empty;

            content = File.ReadAllText(filePath);
            
            return content;
        }
        
        public void SaveFile(string filePath, string content)
        {
            File.WriteAllText(filePath, content);
        }

		public bool IsSupportedFile(string filePath)
		{
            try
            {
                return IsText(filePath);
            }
            catch (Exception ex)
            {
                return false;
            }
		}

        public bool IsText(string filePath)
        {
            return !FileFormat.IsBinary(filePath);
        }
    }
}

