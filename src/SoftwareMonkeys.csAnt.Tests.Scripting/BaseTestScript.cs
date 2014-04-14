using System;
using NUnit.Framework;
using System.IO;

namespace SoftwareMonkeys.csAnt.Tests.Scripting
{
    [TestFixture]
    [Category("TestScript")]
    public abstract class BaseTestScript : BaseScript, ITestScript
    {
        public TestSummarizer TestSummarizer { get;set; }

        public string TestGroupName { get;set; }

        public BaseTestScript ()
        {
            Constructor = new ScriptingTestScriptConstructor(this);

            // Don't call the construct function if no parameters were provided. Use delayed construction, and let the calling code call the Construct function explicitly.
        }

        public BaseTestScript (string scriptName)
        {
            Constructor = new ScriptingTestScriptConstructor(this);
            
            Construct(scriptName, null);
        }
        
        public BaseTestScript (string scriptName, IScript parentScript)
        {
            Constructor = new ScriptingTestScriptConstructor(this);

            Construct(scriptName, parentScript);
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            Constructor.Construct(scriptName, parentScript);

            SetUp();
        }

        [Test]
        public void Start()
        {
            // Fix the current directory to move into the project root. Otherwise the "bin/[mode]" directory is used as the current directory, which causes errors.
            CurrentDirectory = Path.GetDirectoryName(Path.GetDirectoryName(CurrentDirectory));

            Construct(GetType().Name, null);
            
            Start(new string[]{});
        }
    }
}

