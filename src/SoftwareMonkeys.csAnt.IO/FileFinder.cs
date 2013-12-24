using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class FileFinder : IFileFinder
    {
        public string WorkingDirectory { get; set; }

        public bool IsVerbose { get;set; }

        public IConsoleWriter Console { get; set; }

        public FileFinder (string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
        }

        public FileFinder (
            string workingDirectory,
            bool isVerbose,
            IConsoleWriter console
        )
        {
            WorkingDirectory = workingDirectory;
            IsVerbose = isVerbose;
            Console = console;
        }

        public string[] FindFiles (params string[] patterns)
        {
            return FindFiles(WorkingDirectory, patterns);
        }

        public string[] FindFiles (string baseDirectory, params string[] patterns)
        {
            if (String.IsNullOrEmpty(baseDirectory))
                throw new ArgumentException("baseDirectory", "The base directory must be specified.");

            if (patterns == null || patterns.Length == 0)
                throw new ArgumentException("patterns", "At least one pattern must be specified.");

            List<string> files = new List<string> ();

            foreach (string pattern in patterns) {
                if (!String.IsNullOrEmpty (pattern)) {
                    foreach (string file in FindFiles(baseDirectory, pattern))
                        if (!files.Contains (file))
                            files.Add (file);
                }
            }
            return files.ToArray();
        }

        public string[] FindFiles (string pattern)
        {
            return FindFiles(WorkingDirectory, pattern);
        }

        /// <summary>
        /// Finds files in the specified directory based on the provided pattern.
        /// If no wildcard is found in the pattern it treats it as a single file name.
        /// If the pattern starts with a slash then the SearchOption.AllDirectories option is NOT used.
        /// If the pattern does NOT start with a slash then the SearchOption.AllDirectories option IS used.
        /// </summary>
        public string[] FindFiles (string baseDirectory, string pattern)
        {
            if (String.IsNullOrEmpty (baseDirectory))
                throw new ArgumentException ("baseDirectory", "The base directory must be specified.");

            if (String.IsNullOrEmpty (pattern))
                throw new ArgumentException ("pattern", "The pattern must be specified.");

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Finding files...");
                Console.WriteLine ("  Directory:");
                Console.WriteLine ("  " + baseDirectory);
            }

            List<string> foundFiles = new List<string> ();
            
            if (pattern.IndexOf (baseDirectory) == -1) {
                pattern = baseDirectory.TrimEnd (Path.DirectorySeparatorChar)
                    + Path.DirectorySeparatorChar
                    + pattern.Trim (Path.DirectorySeparatorChar);
            }

            if (IsVerbose) 
                Console.WriteLine ("  Pattern: " + pattern);

            var subPath = pattern.Substring (
                0,
                pattern.LastIndexOf (Path.DirectorySeparatorChar)
            );
            
            if (IsVerbose)
                Console.WriteLine ("  Sub path: " + subPath);

            var subPattern = pattern;

            if (subPattern.IndexOf (Path.DirectorySeparatorChar) > -1) {
                subPattern = pattern.Substring (
                pattern.LastIndexOf (Path.DirectorySeparatorChar),
                pattern.Length - pattern.LastIndexOf (Path.DirectorySeparatorChar)
                ).Trim (Path.DirectorySeparatorChar);
            }
                
            if (IsVerbose)
                Console.WriteLine ("  Sub pattern: " + subPattern);

            var searchOption = SearchOption.TopDirectoryOnly;

            if (subPattern.IndexOf ("**") == 0) {
                subPattern = subPattern.Replace ("**", "*");
                searchOption = SearchOption.AllDirectories;
            }
                
            if (IsVerbose)
                Console.WriteLine ("  Fixed sub pattern: " + subPattern);

            if (Directory.Exists (subPath)) {
                foreach (var file in Directory.GetFiles (subPath, subPattern, searchOption)) {
                    if (!foundFiles.Contains (file))
                        foundFiles.Add (file);
                }
            } else {
                if (IsVerbose) {
                    Console.WriteLine ("Can't find directory:");
                    Console.WriteLine (subPath);
                }
            }
            
            if (IsVerbose) {
                Console.WriteLine ("  # files found: " + foundFiles.Count);
                Console.WriteLine("");
            }

            return foundFiles.ToArray();

        }
    }
}

