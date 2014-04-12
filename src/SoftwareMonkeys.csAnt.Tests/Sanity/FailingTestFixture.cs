using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests.Sanity
{
	[TestFixture]
	public class FailingTestFixture : BaseSanityTestFixture
	{
		[Test]
		public void Test_Fail()
		{
			throw new Exception("Intentionally failing, to show that an exception will show up as a failed test.");
		}
	}
}

