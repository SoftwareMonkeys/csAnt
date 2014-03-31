using System;
namespace SoftwareMonkeys.csAnt.Processes
{
    static public class Platform
    {
        static public bool IsMono
        {
            get
            {
                return Type.GetType("Mono.Runtime") != null;
            }
        }
        
        static public bool IsLinux
        {
            get
            {
                int p = (int) Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }

        static public bool IsWindows
        {
            get
            {
                return (Environment.OSVersion.Platform == PlatformID.Win32NT);
            }
        }
    }
}

