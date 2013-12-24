using System;

namespace SoftwareMonkeys.csAnt.InstallConsole
{
    public class PackageParser
    {
        public string GetRelativeFileFromItem (string item)
        {
            var parts = item.Split (',');

            var relativeFile = String.Empty;

            if (parts.Length >= 1) {
                relativeFile = parts[0].Trim ();
            }

            return relativeFile;
        }

        public string GetLocationPathFromItem (string item)
        {
            var parts = item.Split (',');

            var locationPath = String.Empty;

            if (parts.Length >= 2) {
                locationPath = parts[1].Trim ();
            }

            return locationPath;
        }

        public string GetUnzipDestinationFromLine (string item)
        {
            var parts = item.Split (',');

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

        public string GetUnzipSubPathFromLine (string item)
        {
            var parts = item.Split (',');

            var subPath = String.Empty;

            if (parts.Length >= 4) {
                subPath = parts [3].Trim ();
            }

            return subPath;
        }
    }
}

