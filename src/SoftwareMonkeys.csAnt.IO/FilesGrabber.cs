using System;
using System.Collections.Generic;
using System.IO;

namespace SoftwareMonkeys.csAnt.IO
{
    public class FilesGrabber : IFilesGrabber
    {
        public string OriginalDirectory { get; set; }

        public string CurrentDirectory { get;set; }
                
        public FileCopier Copier { get;set; }

        public bool IsVerbose { get;set; }
        
        public string[] LibFilePatterns
        {
            get
            {
                return new string[] {
                    "csAnt-SetUp.exe",
                    "csAnt-SetUpFromLocal.exe",
                    "lib/nuget.exe",
                    "lib/csAnt/**",
                    "lib/FileNodes/**",
                    "lib/HtmlAgilityPack/**",
                    "lib/cs-script/**",
                    "lib/SharpZipLib/**",
                    "lib/ILRepack.1.25.0/**",
                    "lib/Newtonsoft.Json.6.0.1/lib/net40/**",
                    "lib/NUnit/bin/nunit.framework.dll"
                };
            }
        }
        public string[] SrcFilePatterns
        {
            get
            {
                return new string[] {
                    "src/**.node",
                    "src/**.cs",
                    "src/**.csproj",
                    "src/**.sln",
                    "src/**.snk"
                };
            }
        }
        public string[] BinFilePatterns
        {
            get
            {
                return new string[] {
                    "bin/**"
                };
            }
        }

        public string[] PackageSpecFilePatterns
        {
            get
            {
                return new string[] {
                    "pkg/*.nuspec"
                };
            }
        }

        public string[] ScriptFilePatterns
        {
            get
            {
                return new string[] {
                    "scripts/**"
                };
            }
        }

        public string[] LauncherFilePatterns
        {
            get
            {
                return new string[] {
                    "*.sh",
                    "*.bat",
                    "*.vbs"
                };
            }
        }

        public string[] NodeFilePatterns
        {
            get
            {
                return new string[] {
                    "*.node",
                    "src/*.node",
                    "lib/*.node",
                    "!_security/"
                };
            }
        }

        public string[] MiscFilePatterns
        {
            get
            {
                return new string[] {
                    "readme.txt"
                };
            }
        }

        public FilesGrabber (
            string originalDirectory,
            string currentDirectory
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Copier = new FileCopier(originalDirectory, currentDirectory);
        }
        
        public FilesGrabber (
            string originalDirectory,
            string currentDirectory,
            bool isVerbose
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Copier = new FileCopier(originalDirectory, currentDirectory);
            IsVerbose = isVerbose;
        }

        public void GrabInstallation()
        {
            // TODO: Should installation files be more restricted?
            GrabOriginalFiles();
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
            Grab(LauncherFilePatterns);
            Grab(ScriptFilePatterns);
            Grab(LibFilePatterns);
        }
        
        public void GrabOriginalLibFiles ()
        {
            Grab(
                LibFilePatterns
            );
        }
        public void GrabOriginalFiles ()
        {
            Grab(LauncherFilePatterns);

            Grab(NodeFilePatterns);

            Grab(LibFilePatterns);

            Grab(SrcFilePatterns);

            Grab(BinFilePatterns);

            Grab(ScriptFilePatterns);
            
            Grab(PackageSpecFilePatterns);

            Grab(MiscFilePatterns);
        }

        public void GrabOriginalFiles (params string[] patterns)
        {
            Grab(patterns);
        }

        public void Grab(params string[] patterns)
        {
            Copier.Copy(patterns);
        }
        
        public void GrabLibFiles()
        {
            GrabOriginalFiles(LibFilePatterns);
        }
        
        public void GrabSrcFiles()
        {
            GrabOriginalFiles(SrcFilePatterns);
        }

    }
}

