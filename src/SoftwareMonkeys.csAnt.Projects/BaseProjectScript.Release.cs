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
        
        public void Release(string releaseList)
        {
            var cmd = new GenerateProjectReleaseZipCommand(this, releaseList);

            cmd.ReleaseList = releaseList;

            ExecuteCommand(
                cmd
            );
        }
    }
}

