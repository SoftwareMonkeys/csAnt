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
            var name = "csAnt-setupfromlocal";

            new FilesGrabber(
                source,
                destination
                ).GrabOriginalFiles(
                    name + ".sh",
                    name + ".vbs"
                );
        }
    }
}

