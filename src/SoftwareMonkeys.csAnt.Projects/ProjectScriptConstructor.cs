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

            script.InitializeVersionManager(new VersionManager());

            script.InitializeNodeManager(new ProjectNodeManager());

            base.Construct (scriptName, parentScript);
        }

        public override void ConstructNodeManager ()
        {
            Script.Nodes = new ProjectNodeManager();
        }
    }
}

