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
            var scriptsDir = WorkingDirectory
                + Path.DirectorySeparatorChar
                + "scripts";

            if (!pattern.EndsWith(".cs"))
                pattern += ".cs";

            var scripts = Finder.FindFiles (scriptsDir, pattern);

            var scriptNames = new List<string>();

            foreach (var script in scripts) {
                scriptNames.Add (Path.GetFileNameWithoutExtension(script));
            }

            return scriptNames.ToArray();
        }
    }
}

