using System;
namespace SoftwareMonkeys.csAnt
{
    public class BuildMode
    {
        private bool isDebug;
        public bool IsDebug
        {
            get
            {
                return isDebug;
            }
            set { isDebug = value; }
        }

        public string Value
        {
            get
            {
                if (IsDebug)
                    return "Debug";
                else
                    return "Release";
            }
        }

        public BuildMode()
        {
#if DEBUG
                IsDebug = true;
#else
                IsDebug = false;
#endif
        }
    }
}

