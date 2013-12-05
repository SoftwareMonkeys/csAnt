using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt
{
    public class FilesGrabber
    {
        public IScript Script { get;set; }

        public IConsoleWriter Console { get; set; }

        public FilesGrabber (IScript script)
        {
            Script = script;
            Console = script.Console;
        }

        public void GrabOriginalScripts (
            params string[] scriptNames
        )
        {
            List<string> list = new List<string> ();

            foreach (var name in scriptNames) {
                list.Add ("/scripts/" + name + ".cs");
                list.Add ("/scripts/" + name + "/**.cs");
            }

            GrabOriginalFiles(
                list.ToArray()
            );
        }

        public void GrabOriginalScriptingFiles ()
        {
            GrabOriginalFiles(
                Script.CurrentDirectory,
                "../*.node",
                "/*.exe",
                "/*.node",
                "/*.sh",
                "/*.bat",
                "/*.vbs",
                "/bin/**",
                "/lib/**",
                "/src/**.cs",
                "/src/**.csproj",
                "/src/**.sln",
                "/scripts/**"
            );
        }
        
        public void GrabOriginalFiles ()
        {
            GrabOriginalFiles(
                Script.CurrentDirectory,
                "/*",
                "/lib/**",
                "/bin/**",
                "/src/**.cs",
                "/src/**.csproj",
                "/src/**.sln",
                "/scripts/**",
                "/rls/*.txt",
                "/rls/*.zip",
                "../*.node"
            );
        }

        public void GrabOriginalFiles (params string[] patterns)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Grabbing original project files...");
                Console.WriteLine ("From:");
                Console.WriteLine ("  " + Script.OriginalDirectory);
                Console.WriteLine ("To:");
                Console.WriteLine ("  " + Script.CurrentDirectory);
                Console.WriteLine ("");
            }

            if (Script.CurrentDirectory != Script.OriginalDirectory) {
                foreach (var file in Script.FindFiles (Script.OriginalDirectory, patterns)) {

                    var toFile = file.Replace (Script.OriginalDirectory, Script.CurrentDirectory);

                    if (!Directory.Exists (Path.GetDirectoryName (toFile)))
                        Directory.CreateDirectory (Path.GetDirectoryName (toFile));

                    if (Script.IsVerbose)
                    {
                        Console.WriteLine ("From:");
                        Console.WriteLine ("  " + file);
                        Console.WriteLine ("To:");
                        Console.WriteLine ("  " + toFile);
                    }

                    File.Copy (file, toFile, true);
                }
            }
            else
                Console.WriteLine ("OriginalDirectory is the same as CurrentDirectory. No need to grab files.");

            Console.WriteLine ("");
        }
    }
}

