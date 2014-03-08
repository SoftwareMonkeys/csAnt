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

            script.InitializeVersionManager(new VersionManager());

            script.InitializeNodeManager(new ProjectNodeManager());
        }
    }
}

