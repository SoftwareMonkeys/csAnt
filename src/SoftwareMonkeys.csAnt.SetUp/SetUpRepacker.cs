using System;
namespace SoftwareMonkeys.csAnt.SetUp
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
                "lib/SharpZipLib/net-20/ICSharpCode.SharpZipLib.dll",
                "lib/HtmlAgilityPack/Net40/HtmlAgilityPack.dll",
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

