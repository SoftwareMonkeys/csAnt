using System;
using SoftwareMonkeys.csAnt.IO;
using System.IO;
using System.Collections.Generic;


namespace SoftwareMonkeys.csAnt
{
    public class ScriptFinder
    {
        private string workingDirectory;
        public string WorkingDirectory
        {
            get
            {
                if (String.IsNullOrEmpty(workingDirectory))
                    return Environment.CurrentDirectory;
                else
                    return workingDirectory;
            }
            set { workingDirectory = value; }
        }

        public IFileFinder Finder { get;set; }

        public ScriptFinder ()
        {
            Finder = new FileFinder();
        }

        public string[] Find(string pattern)
        {
            var list = new List<string> ();

            list.AddRange (FindStandardScripts (pattern));

            list.AddRange (FindAppScripts (pattern));

            return list.ToArray ();
        }

        public string[] FindStandardScripts(string pattern)
        {
            var scriptNames = new List<string>();

            var scriptsDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                    + "scripts";

            if (Directory.Exists (scriptsDir)) {
                if (!pattern.EndsWith (".cs"))
                    pattern += ".cs";

                var scripts = Finder.FindFiles (scriptsDir, pattern);

                foreach (var script in scripts) {
                    scriptNames.Add (Path.GetFileNameWithoutExtension (script));
                }
            }

            return scriptNames.ToArray();
        }
        

        public string[] FindAppScripts(string pattern)
        {
            var scriptsDirs = GetAppScriptsDirs();

            if (!pattern.EndsWith(".cs"))
                pattern += ".cs";
            
            var scriptNames = new List<string> ();

            foreach (var scriptsDir in scriptsDirs) {
                var scripts = Finder.FindFiles (scriptsDir, pattern);

                foreach (var script in scripts) {
                    scriptNames.Add (Path.GetFileNameWithoutExtension (script));
                }
            }

            return scriptNames.ToArray();
        }

        public string[] GetAppScriptsDirs()
        {
            var list = new List<string> ();

            var appsDir = Path.GetFullPath ("apps");

            if (Directory.Exists (appsDir)) {
                foreach (var appDir in Directory.GetDirectories(appsDir)) {
                    var scriptsDir = appDir + Path.DirectorySeparatorChar + "scripts";

                    if (Directory.Exists (scriptsDir))
                        list.Add (scriptsDir);
                }
            }

            return list.ToArray();
        }
    }
}

