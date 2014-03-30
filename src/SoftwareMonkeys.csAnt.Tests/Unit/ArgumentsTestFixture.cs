using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
	[TestFixture]
	public class ArgumentsTestFixture
	{
		// TODO: Remove if not needed
		/*[Test]
		public void Test_this_Indexer()
		{
			var firstVar = "FirstVar";

			var secondVar = "SecondVar";

			var arguments = new Arguments(
				new string[]{
					firstVar,
					"-s:" + secondVar
				}
			);

			Assert.AreEqual(firstVar, arguments[0], "Indexer didn't return the correct value.");
		}*/
		
		[Test]
		public void Test_this_Key()
		{
			var firstVar = "FirstVar";

			var secondVar = "SecondVar";

			var arguments = new Arguments(
				new string[]{
					firstVar,
					"-s:" + secondVar
				}
			);

			Assert.AreEqual(secondVar, arguments["s"], "Accessing by key didn't return the correct value.");

		}
	}
}

