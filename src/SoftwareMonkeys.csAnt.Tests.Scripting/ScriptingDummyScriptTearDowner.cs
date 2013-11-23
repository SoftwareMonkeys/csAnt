using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class ScriptingDummyScriptTearDowner : DummyScriptTearDowner
    {
        public ScriptingDummyScriptTearDowner(IDummyScript script) : base(script)
        {}

        public override void TearDown()
        {
            throw new NotImplementedException();
            /*
            Script.ReportGenerator.GenerateReports();

            base.TearDown(Script);*/
        }
    }
}

