using System;

namespace SoftwareMonkeys.csAnt.Tests
{
    public class BaseScriptConstructor : IScriptConstructor
    {
        IScript Script { get; set; }

        public BaseScriptConstructor (IScript script)
        {
            Script = script;
        }

        public void Construct()
        {
            Script.IsVerbose = true;
            Script.StopOnFail = false;

            Script.Grabber = new TestFilesGrabber(Script);

            Script.SetUpper = new DummyScriptSetUpper();

            Script.TearDowner = new DummyScriptTearDowner();
        }
    }
}

