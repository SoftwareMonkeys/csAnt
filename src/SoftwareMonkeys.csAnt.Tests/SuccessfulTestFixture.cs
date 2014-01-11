using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class SuccessfulTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Succeed()
        {
            SetUp ();

            Console.WriteLine ("");
            Console.WriteLine ("Starting successful test...");

            var script = GetDummyScript("TestScript");

            script.ConsoleWriter.WriteLine ("...");

            Assert.IsTrue(true);
        }
    }
}

