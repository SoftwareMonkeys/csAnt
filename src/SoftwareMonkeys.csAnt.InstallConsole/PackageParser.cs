using System;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
    public class InstallListParser
    {
        static public string GetRelativeFileFromLine (string line)
        {
            var parts = line.Split (',');

            var relativeFile = String.Empty;

            if (parts.Length >= 1) {
                relativeFile = parts[0].Trim ();
            }

            return relativeFile;
        }

        static public string GetLocationPathFromLine (string line)
        {
            var parts = line.Split (',');

            var locationPath = String.Empty;

            if (parts.Length >= 2) {
                locationPath = parts[1].Trim ();
            }

            return locationPath;
        }

        static public string GetUnzipDestinationFromLine (string line)
        {
            var parts = line.Split (',');

            var destination = String.Empty;

            if (parts.Length >= 3) {
                destination = parts [2].Trim ();
                // TODO: Remove if not needed
/*
                destination = InstallPath.TrimEnd (Path.DirectorySeparatorChar)
                    + Path.DirectorySeparatorChar
                    + destination.Trim (Path.DirectorySeparatorChar);*/
            }
            return destination;
        }

        public string GetUnzipSubPathFromLine (string line)
        {
            var parts = line.Split (',');

            var subPath = String.Empty;

            if (parts.Length >= 4) {
                subPath = parts [3].Trim ();
            }

            return subPath;
        }
    }
}

