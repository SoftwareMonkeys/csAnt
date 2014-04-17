using System;
using SoftwareMonkeys.csAnt.IO;


namespace SoftwareMonkeys.csAnt.SetUp.Deploy.Retrieve
{
    public class SetUpFromLocalConsoleRetriever
    {
        public SetUpFromLocalConsoleRetriever ()
        {
        }

        public void Retrieve(string source, string destination)
        {
            new FilesGrabber(
                source,
                destination
                ).GrabOriginalFiles(
                    "csAnt-SetUpFromLocal.exe"
                );
        }
    }
}

