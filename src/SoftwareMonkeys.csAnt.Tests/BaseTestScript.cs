using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public abstract class BaseTestScript : BaseScript
	{
		public BaseTestScript ()
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

		public bool ShowResult()
		{
			// TODO: Remove this function if not needed.
			// Shouldn't be needed because the RunTestScripts script outputs the
			// result so the script doesnt need to

			/*if (!IsError)
				Console.WriteLine ("Test succeeded.");
			else
				Console.WriteLine ("Test failed.");*/

			return !IsError;
		}
	}
}

