using System;
namespace SoftwareMonkeys.csAnt.Tests
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
                    "lib/FileNodes/**",
                    "lib/NUnit.2.6.3/**",
                    "lib/NUnitResults/**",
                    "lib/HtmlAgilityPack.1.4.6/lib/Net40/**",
                    "lib/SharpZipLib.0.86.0/lib/20/**",
                    "lib/ILRepack.1.25.0/**",
                    "scripts/**",
                    "csAnt.sh"
                };
            }
        }
    }
}

