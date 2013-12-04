using System;
using System.IO;
using System.Collections.Generic;
using SoftwareMonkeys.csAnt.InstallConsole;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public void InstallTo (string name, string directory)
        {
            InstallTo (name, directory, false);
        }

        public void InstallTo(string name, string directory, bool overwriteFiles)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Installing '" + name + "' install to...");
            Console.WriteLine (directory);

            EnsureDirectoryExists(directory);

            var installer = new csAntInstaller(
                directory,
                overwriteFiles,
                IsVerbose
            );

            installer.Install(name);

            /*var exeFile = CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "csAnt-Install.exe";

            List<string> args = new List<string>();

            args.Add("-l " + name);

            args.Add("-d '" + directory + "'");

            if (overwriteFiles)
                args.Add("-o");

            if (IsVerbose)
                args.Add("-v");

            StartProcess(
                exeFile,
                args.ToArray()
            );*/

            Console.WriteLine ("");
        }
    }
}

