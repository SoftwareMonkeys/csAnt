using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class TestFilesGrabber
    {
        public IDummyScript Script { get;set; }

        public TestFilesGrabber (IDummyScript script)
        {
            Script = script;
        }

        public void GrabOriginalScripts (
            params string[] scriptNames
        )
        {
            List<string> list = new List<string> ();

            foreach (var name in scriptNames) {
                list.Add ("/scripts/" + name + ".cs");
                list.Add ("/scripts/" + name + "/**");
            }

            GrabOriginalFiles(
                Script.CurrentDirectory,
                list.ToArray()
            );
        }

        public void GrabOriginalScriptingFiles ()
        {
            GrabOriginalFiles(
                Script.CurrentDirectory,
                "/*.node",
                "/*.sh",
                "/*.bat",
                "/*.vbs",
                "/lib/**.dll",
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
                "/src/**.cs",
                "/src/**.csproj",
                "/src/**.sln",
                "/scripts/**",
                "/rls/*.txt"
            );
        }

        public void GrabOriginalFiles (string workingDirectory, params string[] patterns)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Grabbing original project files...");
                Console.WriteLine ("From:");
                Console.WriteLine (Script.OriginalDirectory);
                Console.WriteLine ("To:");
                Console.WriteLine (workingDirectory);
                Console.WriteLine ("");
            }

            foreach (var file in Script.FindFiles (Script.OriginalDirectory, patterns)) {
                Console.WriteLine (file.Replace(Script.OriginalDirectory, ""));

                var toFile = file.Replace(Script.OriginalDirectory, workingDirectory);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                File.Copy (file, toFile, true);
            }
        }
    }
}

