using System;
using System.IO;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseDummyScript : BaseScript, IDummyScript
	{
		public TestSummarizer TestSummarizer { get;set; }

		public string TestGroupName { get;set; }

		public BaseDummyScript () : base()
		{
		}

		public BaseDummyScript (string scriptName) : base(scriptName)
		{
		}

        public BaseDummyScript (string scriptName, IScript parentScript) : base(scriptName, parentScript)
        {
        }

		public override void Construct (string scriptName, IScript parentScript)
		{
            Constructor = new DummyScriptConstructor(this);
            Constructor.Construct(scriptName, parentScript);

            SetUp();
		}

		public void Fail(string message)
		{
			Error (message);
		}

		public void AssertIsTrue(bool value, string message)
		{
			if (!value)
				Fail (message);
		}
	}
}

