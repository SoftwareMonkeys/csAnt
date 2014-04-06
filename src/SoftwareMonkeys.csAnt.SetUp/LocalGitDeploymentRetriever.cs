using System;
using SoftwareMonkeys.csAnt.SourceControl.Git;


namespace SoftwareMonkeys.csAnt.SetUp
{
    public class LocalGitDeploymentRetriever : BaseDeploymentFilesRetriever
    {
        public Gitter Git { get;set; }

        public LocalGitDeploymentRetriever ()
        {
            Git = new Gitter();
        }

        public override void Retrieve(string source, string destination)
        {
            Git.Clone(source, destination);
        }
    }
}

