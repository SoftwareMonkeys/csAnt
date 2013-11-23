using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class GenerateProjectReleasesZipCommand : BaseProjectScriptCommand
    {
        public GenerateProjectReleasesZipCommand (BaseProjectScript script) : base(script)
        {
        }

        public override void Execute ()
        {
            var listDir = Script.CurrentDirectory
                + Path.DirectorySeparatorChar
                    + "rls";

            // Loop through the folder containing release list files
            foreach (string listFile in Directory.GetFiles(listDir, "*-list.txt"))
            {
                var cmd = new GenerateProjectReleaseZipCommand(Script, listFile);

                Script.ExecuteCommand(cmd);

            }
        }
    }
}

