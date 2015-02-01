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
                    "lib/FileNodes.0.6.5/*.node",
                    "lib/FileNodes.0.6.5/bin/Release/*.dll",
                    "lib/HtmlAgilityPack.1.4.6/lib/Net40/**",
                    "lib/CS-Script.3.7.2.0/lib/net40/**",
                    "lib/SharpZipLib.0.86.0/lib/20/**",
                    "lib/ILRepack.1.25.0/**",
                    "lib/Newtonsoft.Json.6.0.2/lib/net40/**",
                    "lib/NUnit.2.6.0.12051/lib/**",
                    "lib/NUnit.Runners.2.6.0.12051/tools/**",
                    "lib/NUnitResults.1.1/bin/*",
                    "lib/Nuget.Core.2.8.1/lib/net40-Client/*",
                    "lib/Microsoft.Web.Xdt.1.0.0/lib/net40/**" 
                };
            }
        }
        
        public string[] LibPackageFilePatterns
        {
            get
            {
                return new string[] {
                    "lib/csAnt/*.nupkg",
                    "lib/FileNodes.0.6.5/*.nupkg",
                    "lib/HtmlAgilityPack.1.4.6/*.nupkg",
                    "lib/CS-Script.3.7.2.0/*.nupkg",
                    "lib/SharpZipLib.0.86.0/*.nupkg",
                    "lib/ILRepack.1.25.0/*.nupkg",
                    "lib/Newtonsoft.Json.6.0.2/*.nupkg",
                    "lib/NUnit.2.6.0.12051/*.nupkg",
                    "lib/NUnit.Runners.2.6.0.12051/*.nupkg",
                    "lib/NUnitResults.1.1/*.nupkg",
                    "lib/Nuget.Core.2.8.1/*.nupkg",
                    "lib/Microsoft.Web.Xdt.1.0.0/*.nupkg" 
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
                    "bin/*",
                    "bin/packed/*"
                };
            }
        }

        public string[] PackageFilePatterns
        {
            get
            {
                var list = new List<string>();
                list.Add("pkg/**.nupkg");
                list.AddRange(PackageSpecFilePatterns);
                return list.ToArray();
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
                    "readme.txt",
                    "src/TimeStamps.txt"
                };
            }
        }
        
        public string[] TestResultsPatterns
        {
            get
            {
                return new string[] {
                    "tests/results/**"
                };
            }
        }

        public string[] AppFilePatterns
        {
            get {
                return new string[] {
                    "apps/**"
                };
            }
        }

        private bool overwrite;
        public bool Overwrite
        {
            get
            {
                return overwrite; 
            }
            set
            {
                overwrite = value;
                Copier.Overwrite = value;
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
            bool overwrite
        )
        {
            OriginalDirectory = originalDirectory;
            CurrentDirectory = currentDirectory;
            Copier = new FileCopier(originalDirectory, currentDirectory, Overwrite);
            Overwrite = overwrite;
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
            Grab(AppFilePatterns);
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

            Grab(LibPackageFilePatterns);

            Grab(SrcFilePatterns);

            Grab(BinFilePatterns);

            Grab(ScriptFilePatterns);
            
            Grab(PackageFilePatterns);

            Grab(MiscFilePatterns);

            Grab(TestResultsPatterns);

            Grab (AppFilePatterns);
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

