using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void DeployRelease (string destination)
        {
            DeployRelease("project-release", destination);
        }

        public void DeployRelease(string releaseList, string destination)
        {
            var cmd = new DeployReleaseCommand(
                this,
                releaseList,
                destination
            );

            ExecuteCommand(cmd);
        }
    }
}

