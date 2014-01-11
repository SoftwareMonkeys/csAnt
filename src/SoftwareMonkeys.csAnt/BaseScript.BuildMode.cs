using System;

namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string BuildMode
        {
            get
            {
                if (IsDebug)
                    return "Debug";
                else
                    return "Release";
            }
        }
    }
}

