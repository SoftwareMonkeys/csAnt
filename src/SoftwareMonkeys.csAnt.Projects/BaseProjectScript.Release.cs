using System;

namespace SoftwareMonkeys.csAnt.Projects
{
    public partial class BaseProjectScript
    {
        public void Release()
        {
            ExecuteCommand(
                new GenerateProjectReleasesZipCommand(this)
            );
        }
        
        public void Release(string releaseName)
        {
            var cmd = new GenerateProjectReleaseZipCommand(this, releaseName);

            cmd.ReleaseName = releaseName;

            ExecuteCommand(
                cmd
            );
        }
    }
}

