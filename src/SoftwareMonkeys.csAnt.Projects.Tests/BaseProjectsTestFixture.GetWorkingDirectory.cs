using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects.Tests
{
    public partial class BaseProjectsTestFixture
    {
        public override string GetWorkingDirectory()
        {
            var wd = base.GetWorkingDirectory();

            var c = Environment.CurrentDirectory;

            var projectName = Path.GetFileName(wd);

            var groupName = Path.GetFileName(Path.GetDirectoryName(c));

            wd = Path.GetDirectoryName(wd)
                + Path.DirectorySeparatorChar
                + groupName
                + Path.DirectorySeparatorChar
                + projectName;

            return wd;
        }
    }
}

