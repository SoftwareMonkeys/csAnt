using System;
using SoftwareMonkeys.FileNodes;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectNodeState : FileNodeState
    {
        public new ProjectNodeGetter Getter { get;set; }

        private FileNode groupNode;
        public FileNode GroupNode
        {
            get {
                if (groupNode == null)
                    groupNode =  Getter.GetGroupNode();
                return groupNode;
            }
        }

        public bool GroupNodeFound
        {
            get { return groupNode != null; }
        }

        private FileNode sourceNode;
        public FileNode SourceNode
        {
            get {
                if (sourceNode == null)
                    sourceNode = CurrentNode.Nodes["Source"];
                return sourceNode;
            }
        }

        public bool SourceNodeFound
        {
            get { return sourceNode != null; }
        }


        public ProjectNodeState ()
        {
            Getter = new ProjectNodeGetter(this);
        }
    }
}

