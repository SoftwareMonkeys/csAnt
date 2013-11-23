using System;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    public class BaseTestScriptSetUpper : BaseScriptSetUpper
    {
        public BaseTestScriptSetUpper (IScript script) : base(script)
        {
        }

        public override void SetUp ()
        {
            base.SetUp ();
            
            Script.Relocator.Relocate(Script.GetTmpDir());
        }
    }
}

