using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Packages.CreatorConsole
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("Creating a package:");

          /*  var arguments = new Arguments (args);

            var name = args [0];

            var filePath = "";

            if (args.Length >= 2)
                filePath = Path.GetFullPath (args [1]);
            else
                filePath = Path.GetFullPath (name + ".pkg");

            var creator = new PackageCreator (Environment.CurrentDirectory);

            var package = creator.Create (name, filePath);*/

            throw new NotImplementedException();
            /*if (arguments.Contains ("scan")) {
                var scanner = new PackageFileScanner(package.WorkingDirectory, new FileFinder());

                var files = scanner.Scan ();

                package.Files.AddRange(files);
            }
            var saver = new PackageSaver();
            saver.Save(package);*/
        }
    }
}
