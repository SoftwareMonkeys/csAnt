using System;
using SoftwareMonkeys.csAnt.Versions;

namespace SoftwareMonkeys.csAnt.Projects
{
    public class ProjectScriptConstructor : BaseScriptConstructor
    {
        public ProjectScriptConstructor (IScript script) : base(script)
        {
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            var script = (BaseProjectScript)Script;

            base.Construct (scriptName, parentScript);

            var nodeManager = new ProjectNodeManager();
            if (parentScript != null)
                nodeManager.CurrentNode = parentScript.Nodes.CurrentNode;
            script.InitializeNodeManager(nodeManager);

            script.InitializeVersionManager(new VersionManager(nodeManager.CurrentNode));
        }
    }
}

