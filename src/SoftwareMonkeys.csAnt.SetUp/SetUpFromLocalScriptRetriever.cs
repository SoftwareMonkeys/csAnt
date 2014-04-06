using System;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class SetUpFromLocalScriptRetriever
    {
        public SetUpFromLocalScriptRetriever ()
        {
        }

        public void Retrieve(string source, string destination)
        {
            new FilesGrabber(
                source,
                destination
                ).GrabOriginalFiles(
                    "csAnt-setup-local.sh",
                    "csAnt-setup-local.vbs"
                );
        }
    }
}

