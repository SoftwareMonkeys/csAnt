using System;
using SoftwareMonkeys.csAnt.Tests.Scripts;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripts
{
    public abstract class BaseProjectScriptingTestScript : BaseProjectDummyScript
    {
        public ScriptReportGenerator ReportGenerator { get;set; }
    
        public BaseProjectScriptingTestScript (BaseProjectsTestFixture fixture) : base(fixture)
        {
        }
    }
}

