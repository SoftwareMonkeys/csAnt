using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class FailingTestFixture
	{
		[Test]
		public void Test_Fail()
		{
			throw new Exception("Intentionally failing, to show that an exception will show up as a failed test.");
		}
	}
}

