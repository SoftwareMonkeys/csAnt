using System;
using System.IO;


namespace SoftwareMonkeys.csAnt.IO
{
    public class PathConverter
    {
        static public string ToAbsolute (string relativePath)
        {
            if (!IsAbsolute (relativePath)) {
                return Path.GetFullPath (relativePath);
            } else {
                var absolutePath = relativePath;
                return absolutePath;
            }
        }
        
        static public string ToAbsolute (string basePath, string relativePath)
        {
            if (!IsAbsolute (relativePath)) {
                return basePath.TrimEnd(Path.DirectorySeparatorChar)
                    + Path.DirectorySeparatorChar
                        + relativePath.TrimStart(Path.DirectorySeparatorChar);
            } else {
                var absolutePath = relativePath;
                return absolutePath;
            }
        }

        static public string ToRelative(string absolutePath)
        {
            return absolutePath.Replace(Environment.CurrentDirectory, "").TrimStart('/').TrimStart('\\');
        }

        static public bool IsAbsolute(string path)
        {
            if (Platform.IsWindows) {
                return path.Contains (":");
            } else {
                // TODO: Check if this is the best way of doing this
                return path.StartsWith (Path.DirectorySeparatorChar.ToString());
            }
        }
    }
}

