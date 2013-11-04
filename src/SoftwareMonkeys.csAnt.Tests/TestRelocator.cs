using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public class TestRelocator : IDisposable
	{
		public ITestScript Script { get;set; }

		bool HasReturned = false;

		public TestRelocator (ITestScript script)
		{
			Script = script;
		}

		public void Relocate()
		{
			var tmpDir = Script.GetTmpDir();

			Script.Relocate(tmpDir);
		}

		public void Return ()
		{
			if (!HasReturned) {
				Script.Relocate (Script.OriginalDirectory);
				HasReturned = true;
			}
		}

		public void Dispose()
		{
			Return ();
		}
	}
}

