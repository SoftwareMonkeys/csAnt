using System;
using System.IO;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class XmlResultFileNamer
    {
        private VersionManager versionManager;
        public VersionManager VersionManager
        {
            get
            {
                if (versionManager == null)
                    versionManager = new VersionManager();
                return versionManager;
            }
            set
            {
                versionManager = new VersionManager();
            }
        }

        private Version version;
        public Version Version
        {
            get
            {
                if (version == null)
                    version = new Version(VersionManager.GetVersion(Environment.CurrentDirectory));

                return version;
            }
            set
            {
                version = value;
            }
        }

        public string GetResultsDirectory(IScript script)
        {
            return script.CurrentDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + Version.ToString()
                + Path.DirectorySeparatorChar
                + "xml";
        }
        
        public string GetResultsReturnDirectory(IScript script)
        {
            return script.OriginalDirectory
                + Path.DirectorySeparatorChar
                + "tests"
                + Path.DirectorySeparatorChar
                + "results"
                + Path.DirectorySeparatorChar
                + Version.ToString()
                + Path.DirectorySeparatorChar
                + "xml";
        }
    }
}

