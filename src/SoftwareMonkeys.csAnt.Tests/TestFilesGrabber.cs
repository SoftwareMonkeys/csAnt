using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class TestFilesGrabber
    {
        public ITestScript Script { get;set; }

        public TestFilesGrabber (ITestScript script)
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
                list.ToArray()
            );
        }

        public void GrabOriginalScriptingFiles ()
        {
            GrabOriginalFiles(
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
                "/*",
                "/lib/**.dll",
                "/lib/**.exe",
                "/src/**.cs",
                "/src/**.csproj",
                "/src/**.sln",
                "/scripts/**",
                "/rls/*.txt"
            );
        }

        public void GrabOriginalFiles (params string[] patterns)
        {
            if (Script.IsVerbose) {
                Console.WriteLine ("");
                Console.WriteLine ("Grabbing original project files...");
                Console.WriteLine ("From:");
                Console.WriteLine (Script.OriginalDirectory);
                Console.WriteLine ("To:");
                Console.WriteLine (Script.CurrentDirectory);
                Console.WriteLine ("");
            }

            foreach (var file in Script.FindFiles (Script.OriginalDirectory, patterns)) {
                Console.WriteLine (file.Replace(Script.OriginalDirectory, ""));

                var toFile = file.Replace(Script.OriginalDirectory, Script.CurrentDirectory);

                if (!Directory.Exists(Path.GetDirectoryName(toFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(toFile));

                File.Copy (file, toFile, true);
            }
        }
    }
}

