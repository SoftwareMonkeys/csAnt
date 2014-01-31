using System;
using System.IO;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectCreator
    {
        public NodesCreator NodesCreator { get; set; }

        public INodeState NodeState { get;set; }

        public ProjectCreator (
            INodeState nodeState
            )
        {
            NodesCreator = new NodesCreator(nodeState);
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

