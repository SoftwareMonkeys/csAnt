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
                    "lib/cs-script/**",
                    "lib/FileNodes/**",
                    "lib/NUnit/**",
                    "lib/NUnitResults/**",
                    "lib/HtmlAgilityPack/Net40/**",
                    "lib/SharpZipLib/net-20/**",
                    "lib/ILRepack.1.25.0/**",
                    "scripts/**",
                    "csAnt.sh"
                };
            }
        }
    }
}

