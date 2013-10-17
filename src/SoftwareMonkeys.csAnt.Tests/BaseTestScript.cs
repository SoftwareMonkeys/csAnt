using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseTestScript : BaseScript
	{
		public BaseTestScript () : base()
		{
		}

		public BaseTestScript (string scriptName) : base(scriptName)
		{
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

		public bool Result()
		{
			if (IsError)
				AddSummary ("* " + GetType ().Name + " test script failed.");
			else
				AddSummary (GetType ().Name + " test script succeeded.");

			return !IsError;
		}
	}
}

