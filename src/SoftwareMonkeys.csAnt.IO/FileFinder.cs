using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.IO
{
    public class FileFinder : IFileFinder
    {
        public bool IsVerbose { get;set; }

        public FileFinder ()
        {
        }

        public FileFinder (
            bool isVerbose
        )
        {
            IsVerbose = isVerbose;
        }

        public string[] FindFiles (string workingDirectory, params string[] patterns)
        {
            if (String.IsNullOrEmpty(workingDirectory))
                throw new ArgumentException("workingDirectory", "The working directory must be specified.");

            if (patterns == null || patterns.Length == 0)
                throw new ArgumentException("patterns", "At least one pattern must be specified.");

            List<string> files = new List<string> ();

            foreach (string pattern in patterns) {
                if (!String.IsNullOrEmpty (pattern)) {
                    foreach (string file in FindFilesFromPattern(workingDirectory, pattern))
                        if (!files.Contains (file))
                            files.Add (file);
                }
            }
            return files.ToArray();
        }

        /// <summary>
        /// Finds files in the specified directory based on the provided pattern.
        /// If no wildcard is found in the pattern it treats it as a single file name.
        /// If the pattern starts with a slash then the SearchOption.AllDirectories option is NOT used.
        /// If the pattern does NOT start with a slash then the SearchOption.AllDirectories option IS used.
        /// </summary>
        public string[] FindFilesFromPattern (string workingDirectory, string pattern)
        {
            if (String.IsNullOrEmpty (workingDirectory))
                throw new ArgumentException ("baseDirectory", "The base directory must be specified.");

            if (String.IsNullOrEmpty (pattern))
                throw new ArgumentException ("pattern", "The pattern must be specified.");

            if (IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Finding files...");
                Console.WriteLine ("  Directory:");
                Console.WriteLine ("  " + workingDirectory);
            }

            List<string> foundFiles = new List<string> ();

            FixPathAndPattern(ref workingDirectory, ref pattern);

            if (IsVerbose) 
            {
                Console.WriteLine ("  Specified path: " + workingDirectory);
                Console.WriteLine ("  Specified pattern: " + pattern);
            }

            var searchOption = SearchOption.TopDirectoryOnly;

            if (pattern.IndexOf ("**") == 0) {
                pattern = pattern.Replace ("**", "*");
                searchOption = SearchOption.AllDirectories;
            }

            if (Directory.Exists (workingDirectory)) {
                foreach (var file in Directory.GetFiles (workingDirectory, pattern, searchOption)) {
                    if (!foundFiles.Contains (file))
                        foundFiles.Add (file);
                }
            } else {
                if (IsVerbose) {
                    Console.WriteLine ("Can't find directory:");
                    Console.WriteLine (workingDirectory);
                }
            }
            
            if (IsVerbose) {
                Console.WriteLine ("  # files found: " + foundFiles.Count);
                Console.WriteLine("");
            }

            return foundFiles.ToArray();

        }

        public void FixPathAndPattern(ref string path, ref string pattern)
        {
            var tmp = string.Empty;

            // Fix slashes
            pattern = pattern.Replace('/', Path.DirectorySeparatorChar);
            
            // The following functionality takes the ".." off the pattern so it can be appended to the path
            // TODO: Check if there's a better way to do this

            // Add the working directory to the beginning of the pattern
            if (pattern.IndexOf (path) == -1) {
                tmp = path.TrimEnd (Path.DirectorySeparatorChar)
                    + Path.DirectorySeparatorChar
                    + pattern.Trim (Path.DirectorySeparatorChar);
            }

            // Grab the last section of the value to use as the pattern
            if (tmp.IndexOf (Path.DirectorySeparatorChar) > -1) {
                pattern = tmp.Substring (
                tmp.LastIndexOf (Path.DirectorySeparatorChar),
                tmp.Length - tmp.LastIndexOf (Path.DirectorySeparatorChar)
                ).Trim (Path.DirectorySeparatorChar);
            }

            path = tmp.Substring (
                0,
                tmp.LastIndexOf (Path.DirectorySeparatorChar)
            );

            path = Path.GetFullPath(path);
        }
    }
}

