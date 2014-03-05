using System;
namespace SoftwareMonkeys.csAnt
{
    public partial class BaseScript
    {
        public string VersionAndTimeStamp
        {
            get
            {
                return CurrentNode.Properties["Version"].Replace (".", "-")
                    + "-[" + TimeStamp.Replace(":", "-") + "]";
            }
        }
    }
}

