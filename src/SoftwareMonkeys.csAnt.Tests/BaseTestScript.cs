using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseTestScript : BaseScript
	{
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
	}
}

