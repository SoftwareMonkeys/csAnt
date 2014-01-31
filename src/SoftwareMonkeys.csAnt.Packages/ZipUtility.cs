using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages
{
    public class ZipUtility
    {
        static public bool IsZipFile (string toFile)
        {
            return Path.GetExtension(toFile).Trim('.').ToLower() == "zip";
        }


        public void Unzip (string zipFile, string destination, string subPath)
        {
            /*var tmpDestination = InstallPath
                + Path.DirectorySeparatorChar
                    + "_tmpunzip/";

            var subPathFull = tmpDestination.TrimEnd(Path.DirectorySeparatorChar)
                + Path.DirectorySeparatorChar
                    + subPath.Trim(Path.DirectorySeparatorChar);

            IOUtility.EnsureDirectoryExists(tmpDestination);

            var sharpZipLibPath = GetSharpZipLibPath();

            var assembly = Assembly.LoadFile(sharpZipLibPath);

            var fastZipType = assembly.GetType ("ICSharpCode.SharpZipLib.Zip.FastZip");

            var fastZip = Activator.CreateInstance(fastZipType);

            var nameTransformProperty = fastZipType.GetProperty("NameTransform");

            var zipNameTransformType = assembly.GetType("ICSharpCode.SharpZipLib.Zip.ZipNameTransform");

            var zipNameTransform = Activator.CreateInstance(zipNameTransformType);

            nameTransformProperty.SetValue(fastZip, zipNameTransform, null);

            var method = fastZipType.GetMethod(
                "ExtractZip",
                new Type[]{
                    typeof(string),
                    typeof(string),
                    typeof(string)
                }
            );

            method.Invoke (
                fastZip,
                new object[]{
                    zipFile,
                    tmpDestination,
                    ""
                }
            );

          //  StartProcess(
          //      "mono",
          //      Get7ZipPath() + " x " + zipFile + " -r -o" + tmpDestination
         //   );

            Directory.Move(subPathFull, destination);

            Directory.Delete(tmpDestination);*/
        }

    }
}

