using System;
using SoftwareMonkeys.csAnt.IO;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class DummyScriptConstructor : BaseScriptConstructor
    {
        public DummyScriptConstructor (IDummyScript script) : base(script)
        {
        }

        public override void Construct(string scriptName, IScript parentScript)
        {
            base.Construct(scriptName, parentScript);

            // TODO: Remove if not needed. Should be obsolete
            /*Script.FilesGrabber = new FilesGrabber(
                Script.OriginalDirectory,
                Script.CurrentDirectory
            );*/

            Script.SetUpper = new DummyScriptSetUpper((IDummyScript)Script);

            Script.TearDowner = new DummyScriptTearDowner((IDummyScript)Script);
        }
    }
}

