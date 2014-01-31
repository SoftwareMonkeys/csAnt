using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.SetUpFromWebConsole.cs
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            Console.WriteLine ("");
            Console.WriteLine ("Setting up csAnt and the package manager from online files...");
            Console.WriteLine ("");

            var destinationPath = Environment.CurrentDirectory;

            var arguments = new Arguments(args);

            if (arguments.Contains ("d"))
                destinationPath = Path.GetFullPath(arguments["d"]);

            var overwrite = arguments.Contains("o");

            var list = GetFileList ();

            foreach (var key in list.Keys) {
                var file = destinationPath
                    + Path.DirectorySeparatorChar
                        + key;

                var url = list[key];

                DownloadUtility.Download(url, file, overwrite);
            }
        }

        static public Dictionary<string, string> GetFileList()
        {
            var list = new Dictionary<string, string>();

            list.Add ("/csAnt.bat", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNFBBdGNtdmw0aUE");
            list.Add ("/csAnt.node", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qMXJzZ2g0TlBYM0U");
            list.Add ("/csAnt.sh", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qVWtENHBmSkJuWG8");
            list.Add ("/lib/csAnt/bin/Release/csAnt.exe", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNzZvZlZqbmxDMDg");
            list.Add ("/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qNEo3TUNaRk9nSTA");
            list.Add ("/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qWHBQSXdUcDVXRms");
            list.Add ("/lib/csAnt/bin/Release/SoftwareMonkeys.csAnt.Tests.Scripting.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qSk9qOVZDaXBHRUU");
            list.Add ("/lib/csAnt/bin/Release/SoftwareMonkeys.FileNodes.dll"," https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qa2k4eXc5NVpvQW8");
            list.Add ("/lib/csAnt/bin/Release/Newtonsoft.Json.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qME9LNk9xTW5JNzg");
            list.Add ("/lib/csAnt/bin/Release/CSScriptLibrary.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qZExlZEVYemVjaVk");
            list.Add ("/lib/cs-script/Lib/Bin/NET 4.0/CSScriptLibrary.dll", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qME9LNk9xTW5JNzg");
            list.Add ("/lib/cs-script/cscs.exe", "https://docs.google.com/uc?export=download&id=0B_8QvsLqRy5qVnhfY1lMRWU3Zms");
            list.Add ("/scripts/AddCsAntImport.cs", "https://csant.googlecode.com/git/scripts/AddCsAntImport.cs");
            list.Add ("/scripts/HelloWorld.cs", "https://csant.googlecode.com/git/scripts/HelloWorld.cs");
            list.Add ("/scripts/Update.cs", "https://csant.googlecode.com/git/scripts/Update.cs");

            return list;
        }
    }
}
