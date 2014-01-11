using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void CreateSourceNode()
        {
            CreateNode (
                CurrentDirectory
                + Path.DirectorySeparatorChar
                + "src",
                "Source"
            );
        }
    }
}

