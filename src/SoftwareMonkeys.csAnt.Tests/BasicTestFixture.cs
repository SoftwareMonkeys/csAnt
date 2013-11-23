using System;
using NUnit.Framework;

namespace SoftwareMonkeys.csAnt.Tests
{
    [TestFixture]
    public class BasicTestFixture : BaseTestFixture
    {
        [Test]
        public void Test_Succeed_OutputResults()
        {
            var fixture = new SuccessfulTestFixture();

            fixture.Test_Succeed();
        }
    }
}

