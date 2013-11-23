using System;
using SoftwareMonkeys.csAnt.Tests;

namespace SoftwareMonkeys.csAnt.Projects.Tests.Scripting
{
	public abstract class BaseProjectDummyScript : BaseProjectScript, IDummyScript
	{
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get; set; }

		public BaseProjectDummyScript (string scriptName) : base(scriptName)
		{
            Constructor = new DummyScriptConstructor(this);

            Construct(scriptName);
		}

        public override void Construct(string scriptName)
        {
            base.Construct(scriptName);

            Constructor.Construct(scriptName); 

            SetUp();
        }
	}
}

