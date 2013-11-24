using System;
using SoftwareMonkeys.csAnt.Tests.Scripting;
using SoftwareMonkeys.csAnt.Tests;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
    [TestFixture]
    public abstract partial class BaseProjectTestScript : BaseProjectScript, ITestScript
    {
        public TestSummarizer TestSummarizer { get;set; }

        public string TestGroupName { get;set; }

        public BaseProjectTestScript () : base()
        {
            Constructor = new ProjectTestScriptConstructor(this);
        }

        public BaseProjectTestScript (string scriptName) : base(scriptName)
        {
            Constructor = new ProjectTestScriptConstructor(this);
        }

        public override void Construct (string scriptName, IScript parentScript)
        {
            Constructor = new ProjectTestScriptConstructor(this);
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

