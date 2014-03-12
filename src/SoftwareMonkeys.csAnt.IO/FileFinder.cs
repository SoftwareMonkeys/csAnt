using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading;
using System.Text.RegularExpressions;

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

        public string[] FindFiles (string path, params string[] patterns)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException("path", "The path must be specified.");

            if (patterns == null || patterns.Length == 0)
                throw new ArgumentException("patterns", "At least one pattern must be specified.");

            // Following code is based on an answer from:
            // http://stackoverflow.com/questions/163162/can-you-call-directory-getfiles-with-multiple-filters
            // ... but customized

            if (!Directory.Exists(path)) return new string[] { };

            var include = (from filter in patterns where !string.IsNullOrEmpty(filter.Trim()) select filter.Trim());
            var exclude = (from filter in include where filter.Contains(@"!") select filter);
        
            include = include.Except(exclude);
        
            if (include.Count() == 0) include = new string[] { "*" };
        
            var rxfilters = from filter in exclude select string.Format("^{0}$", filter.Replace("!", "").Replace(".", @"\.").Replace("*", ".*").Replace("?", "."));
            Regex regex = new Regex(string.Join("|", rxfilters.ToArray()));
        
            List<Thread> workers = new List<Thread>();
            List<string> files = new List<string>();
        
            foreach (string filter in include)
            {
                var newPath = path;
                var newFilter = filter;

                FixPathAndPattern(ref newPath, ref newFilter);

                var searchOption = SearchOption.TopDirectoryOnly;
                if (filter.Contains("**"))
                {
                    searchOption = SearchOption.AllDirectories;
                    newFilter = newFilter.Replace("**", "*");
                }

                Thread worker = new Thread(
                    new ThreadStart(
                        delegate
                        {
                            string[] allfiles = Directory.GetFiles(newPath, newFilter, searchOption);
                            if (exclude.Count() > 0)
                            {
                                lock (files)
                                    files.AddRange(allfiles.Where(p => !regex.Match(p).Success));
                            }
                            else
                            {
                                lock (files)
                                    files.AddRange(allfiles);
                            }
                        }
                    ));
        
                workers.Add(worker);
        
                worker.Start();
            }
        
            foreach (Thread worker in workers)
            {
                worker.Join();
            }
        
            return files.ToArray();
        }

        public void FixPathAndPattern(ref string path, ref string pattern)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("A path must be provided.", "path");

            if (string.IsNullOrEmpty(pattern))
                throw new ArgumentException("A pattern must be provided.", "pattern");

            var tmp = string.Empty;

            if (IsVerbose)
            {
                Console.WriteLine ("");
                Console.WriteLine ("Fixing path and pattern");
                Console.WriteLine ("");
                Console.WriteLine ("  Provided...");
                Console.WriteLine ("  Path: " + path);
                Console.WriteLine ("  Pattern: " + pattern);
            }

            var fixedPath = path;
            var fixedPattern = pattern;

            // The following functionality takes the ".." off the pattern so it can be appended to the path
            // TODO: Check if there's a better way to do this

            // Add the working directory to the beginning of the pattern
            if (fixedPattern.IndexOf (fixedPath) == -1) {
                tmp = fixedPath.TrimEnd (Path.DirectorySeparatorChar)
                    + Path.DirectorySeparatorChar
                    + fixedPattern.Trim (Path.DirectorySeparatorChar);
            }

            // Grab the last section of the value to use as the pattern
            if (tmp.IndexOf (Path.DirectorySeparatorChar) > -1) {
                fixedPattern = tmp.Substring (
                tmp.LastIndexOf (Path.DirectorySeparatorChar),
                tmp.Length - tmp.LastIndexOf (Path.DirectorySeparatorChar)
                ).Trim (Path.DirectorySeparatorChar);
            }

            if (tmp.LastIndexOf(Path.DirectorySeparatorChar) > -1)
            {
                fixedPath = tmp.Substring (
                    0,
                    tmp.LastIndexOf (Path.DirectorySeparatorChar)
                );
            }
            else
                fixedPath = tmp;

            if (string.IsNullOrEmpty(fixedPath))
            {
                throw new Exception("Failed to fix path '" + path + "' and pattern '" + fixedPattern + "'. The fixed path was empty.");
            }

            fixedPath = Path.GetFullPath(fixedPath);
            
            if (IsVerbose)
            {
                Console.WriteLine ("");
                Console.WriteLine ("  Fixed...");
                Console.WriteLine ("  Path: " + fixedPath);
                Console.WriteLine ("  Pattern: " + fixedPattern);
                Console.WriteLine ("");
            }

            path = fixedPath;
            pattern = fixedPattern;
        }
    }
}
