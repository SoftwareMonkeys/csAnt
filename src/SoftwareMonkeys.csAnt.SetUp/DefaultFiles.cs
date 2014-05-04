using System;
namespace SoftwareMonkeys.csAnt
{
    static public class DefaultFiles
    {
        static public string[] DefaultFilePatterns
        {
            get
            {
                return new string[]{
                    "lib/csAnt/**",
                    "lib/CS-Script.3.7.2.0/lib/net40/**",
                    "lib/FileNodes.0.6.1/**",
                    "lib/NUnit.2.6.0.12051/lib/nunit.framework.dll",
                    "lib/NUnit.Runners.2.6.0.12051/tools/nunit-console.exe",
                    "lib/NUnitResults.1.1/**",
                    "lib/HtmlAgilityPack.1.4.6/lib/Net40/**",
                    "lib/SharpZipLib.0.86.0/lib/20/**",
                    "lib/ILRepack.1.25.0/**",
                    "lib/Nuget.Core.2.8.1/lib/net40-Client/*",
                    "scripts/**",
                    "csAnt.sh"
                };
            }
        }
    }
}

