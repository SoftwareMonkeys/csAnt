using System;

namespace SoftwareMonkeys.csAnt.Tests
{
	public interface ITestScript : IScript
	{
		// The original starting directory (which may be different from the CurrentDirectory if it is changed)
		string OriginalDirectory { get;set; }

		// The group to put all tests in
		string TestGroupName { get;set; }

		TestRelocator Relocator { get;set; }

		TestFilesGrabber Grabber { get;set; }
	}
}

