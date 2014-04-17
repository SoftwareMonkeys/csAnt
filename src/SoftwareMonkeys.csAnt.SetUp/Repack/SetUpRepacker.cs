using System;
namespace SoftwareMonkeys.csAnt.SetUp.Repack
{
    public class SetUpRepacker
    {
        public string BuildMode = "Release";
        
        public SetUpRepacker ()
        {
        }

        public SetUpRepacker (string buildMode)
        {
            BuildMode = buildMode;
        }

        public void Repack()
        {
            var assemblyFile = "bin/{BuildMode}/csAnt-SetUp.exe";

            var dependencies = new string[]{
                "lib/SharpZipLib.0.86.0/lib/20/ICSharpCode.SharpZipLib.dll",
                "lib/HtmlAgilityPack.1.4.6/lib/Net40/HtmlAgilityPack.dll",
                "lib/CS-Script.3.7.2.0/lib/net40/CSScriptLibrary.dll",
                "lib/CS-Script.3.7.2.0/lib/net40/Mono.CSharp.dll",
                "lib/FileNodes.0.5.3.200/bin/Release/SoftwareMonkeys.FileNodes.dll",
                "lib/Newtonsoft.Json.6.0.2/lib/net40/Newtonsoft.Json.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Contracts.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.IO.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.IO.Contracts.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Imports.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Processes.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.External.Nuget.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.SourceControl.Git.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.SetUp.dll",
                "bin/{BuildMode}/SoftwareMonkeys.csAnt.Versions.dll"
            };

            // TODO: Move repacker to a property
            new Repacker(assemblyFile, dependencies, BuildMode).Repack();
        }
    }
}

