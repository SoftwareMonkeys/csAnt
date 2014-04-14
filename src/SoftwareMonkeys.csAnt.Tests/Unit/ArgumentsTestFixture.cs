using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests.Unit
{
	[TestFixture]
	public class ArgumentsTestFixture
	{		
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
        
        [Test]
        public void Test_ToString()
        {
            var firstVar = "FirstVar";

            var secondVar = "SecondVar";

            var thirdVar = "ThirdVar";

            var arguments = new Arguments(
                firstVar,
                secondVar,
                thirdVar
            );

            var expected = firstVar
                + " "
                + secondVar
                + " "
                + thirdVar;

            Assert.AreEqual(expected, arguments.ToString(), "Incorrect return value.");

        }
	}
}

