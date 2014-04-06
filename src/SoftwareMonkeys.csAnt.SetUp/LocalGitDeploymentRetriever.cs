using System;
using SoftwareMonkeys.csAnt.SourceControl.Git;


namespace SoftwareMonkeys.csAnt.SetUp.Common
{
    public class LocalGitRetriever : BaseDeploymentFilesRetriever
    {
        public Gitter Git { get;set; }

        public LocalGitRetriever ()
        {
            Git = new Gitter();
        }

        public override void Retrieve(string source, string destination)
        {
            Git.Clone(source, destination);
        }
    }
}

