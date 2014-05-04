using System;
using System.IO;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectCreator
    {
        public FileNodesCreator NodesCreator { get; set; }

        public IFileNodeState NodeState { get;set; }

        public ProjectCreator (
            IFileNodeState nodeState
            )
        {
            NodesCreator = new FileNodesCreator(nodeState);
        }

        public void Create (string projectDirectory)
        {
            Create (
                Path.GetFileName(projectDirectory)
            );
        }

        public void Create(string projectName, string projectDirectory)
        {
            NodesCreator.CreateNodes();
        }
    }
}

