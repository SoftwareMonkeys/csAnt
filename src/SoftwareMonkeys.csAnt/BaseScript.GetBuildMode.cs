using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string GetBuildMode()
        {
            if (IsDebug)
                return "Debug";
            else
                return "Release";
        }
    }
}

